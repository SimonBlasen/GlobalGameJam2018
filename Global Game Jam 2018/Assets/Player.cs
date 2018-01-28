using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	[SerializeField]
	private float m_hoverRadius = 5f;
	[SerializeField]
	private float m_constForce = 5f;
	[SerializeField]
	private float m_kickVecYComp = 0.3f;
	[SerializeField]
	private float kickFullChargeTime = 1f;
	[SerializeField]
	private float kick_pushHardBorder = 0.8f;
	[SerializeField]
	private float hover_throw_threshhold_velocity = 5f;
	[SerializeField]
	private float hover_cube_P = 1f;
	[SerializeField]
	private float hover_cube_I = 0.8f;
	[SerializeField]
	private AudioClip[] selfScreamClips;
	[SerializeField]
	private AudioClip[] damageClips;
	[SerializeField]
	private float yoffoJumpAlongDistance = 20f;
	[SerializeField]
	private float yoffoJumpProbability = 0.8f;

	private bool m_isDead = false;

	[Header ("References")]
	[SerializeField]
	private Image image_crosshairOutofrange;
	[SerializeField]
	private Image image_crosshairInrange;
	[SerializeField]
	private Image image_kickCharge;
	[SerializeField]
	private AudioFootsteps audioFootsteps;
	[SerializeField]
	private Image[] images_health;
	[SerializeField]
	private ColliderChecker colliderCheckerScream;
	[SerializeField]
	private AudioSource audioSourceDamage;
	private RigidbodyFirstPersonController fps;


	[Header ("Info")]
	[SerializeField]
	private int health = 3;


	private Transform m_grabbedCubem = null;
	private Vector3 m_grab_relative = Vector3.zero;

	private float kick_charge = 0f;
	private float imageKickChargeX = 0f;
	private float imageKickChargeY = 0f;


	void Start () {
        Cursor.visible = false;
		fps = GetComponent<RigidbodyFirstPersonController>();
		Cursor.lockState = CursorLockMode.Locked;

		imageKickChargeX = image_kickCharge.GetComponent<RectTransform>().sizeDelta.x;
		imageKickChargeY = image_kickCharge.GetComponent<RectTransform>().sizeDelta.y;
    }

	private bool wasJumping = false;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.K)) {
			Die ();
		}
		if (m_isDead) {
//			if (Camera.main.transform.rotation.x)
//			Debug.Log ("Rotation" + Camera.main.transform.rotation.x + " - Color: " + FindObjectOfType<OverlayImage> ().GetComponent<Image> ().color);

			if (FindObjectOfType<OverlayImage> ().GetComponent<Image> ().color.a > 250f)
				m_isDead = false;
			Camera.main.transform.Rotate (Vector3.right * 20f * Time.deltaTime);
			Color newColor = FindObjectOfType<OverlayImage> ().GetComponent<Image> ().color;
			newColor.a = newColor.a + .4f * Time.deltaTime;
			FindObjectOfType<OverlayImage> ().GetComponent<Image> ().color = newColor;
		
		}
		audioFootsteps.WalkingSpeed = (new Vector2 (GetComponent<Rigidbody> ().velocity.x, GetComponent<Rigidbody> ().velocity.z)).magnitude;

	

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		if (kick_charge != 0f) {
			image_kickCharge.GetComponent<RectTransform> ().sizeDelta = new Vector2 (imageKickChargeX * Mathf.Min (1f, (kick_charge / kickFullChargeTime)), imageKickChargeY);
		} else if (image_kickCharge.GetComponent<RectTransform> ().sizeDelta.x != 0f) {
			image_kickCharge.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0f, imageKickChargeY);
		}
			

		RaycastHit[] hitsCrosshair = Physics.RaycastAll (new Ray (Camera.main.transform.position, Camera.main.transform.forward), m_hoverRadius);
		Transform nearestCubie = null;
		if (hitsCrosshair.Length > 0) {
			float nearestDistance = float.MaxValue;

			for (int i = 0; i < hitsCrosshair.Length; i++) {
				if (hitsCrosshair [i].collider.transform.GetComponent<Cubie> () != null) {
					if (hitsCrosshair [i].distance < nearestDistance) {
						nearestDistance = hitsCrosshair [i].distance;
						nearestCubie = hitsCrosshair [i].collider.transform;
					}
				}
			}

			if (nearestCubie != null) {
				image_crosshairInrange.enabled = true;
				image_crosshairOutofrange.enabled = false;
			} else {
				image_crosshairInrange.enabled = false;
				image_crosshairOutofrange.enabled = true;
			}
		} else {
			image_crosshairInrange.enabled = false;
			image_crosshairOutofrange.enabled = true;
		}


		if (Input.GetKeyDown (KeyCode.Q)) {
			PlayScreamAudio ();
			if (colliderCheckerScream.colliderInside.Count > 0) {
				for (int i = 0; i < colliderCheckerScream.colliderInside.Count; i++) {
					colliderCheckerScream.colliderInside [i].GetComponent<Cubie> ().Interact (InteractionType.SCREAM);
				}
			}
		}




		if (Input.GetMouseButtonDown (0)) {
			if (nearestCubie != null && nearestCubie.GetComponent<Cubie> ().FollowTransform != transform) {
				m_grabbedCubem = nearestCubie;
				m_grabbedCubem.GetComponent<Rigidbody> ().useGravity = false;
				m_grab_relative = nearestCubie.position - transform.position;
			}
            
		}

		if (Input.GetMouseButtonUp (0) && m_grabbedCubem != null) {
			m_grabbedCubem.GetComponent<Rigidbody> ().useGravity = true;
			if (m_grabbedCubem.GetComponent<Rigidbody> ().velocity.magnitude > hover_throw_threshhold_velocity) {
				m_grabbedCubem.GetComponent<Cubie> ().Interact (InteractionType.THROW);
			} else {
				m_grabbedCubem.GetComponent<Cubie> ().Interact (InteractionType.PUT_DOWN);
			}
			m_grabbedCubem = null;
		}


		if (m_grabbedCubem) {
			Vector3 toDest = (transform.position + Camera.main.transform.forward * m_hoverRadius) - m_grabbedCubem.transform.position;
			//m_grabbedCubem.GetComponent<Rigidbody>().AddForce(toDest * (Vector3.Distance(m_grabbedCubem.transform.position, (transform.position + Camera.main.transform.forward * m_hoverRadius)) * hover_cube_P + m_grabbedCubem.GetComponent<Rigidbody>().velocity.magnitude * hover_cube_I * Mathf.Cos((Mathf.PI / 180f) * Vector3.Angle(m_grabbedCubem.GetComponent<Rigidbody>().velocity, toDest))));
			//m_grabbedCubem.position = transform.position + Camera.main.transform.forward * m_hoverRadius;
			//m_grabbedCubem.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
		
			RaycastHit[] hits = Physics.RaycastAll (new Ray (transform.position, Camera.main.transform.forward), m_hoverRadius);
			float maxDistance = m_hoverRadius;
			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].collider.tag != "Cube") {
					if (hits [i].distance - 0.6f < maxDistance && hits [i].distance > 1.4f && hits [i].collider.GetType () != typeof(CapsuleCollider) && hits [i].collider.GetType () != typeof(SphereCollider)) {
						maxDistance = hits [i].distance - 0.6f;
					}
				}
			}

			m_grabbedCubem.position = transform.position + Camera.main.transform.forward * maxDistance;
		}






		if (fps.Jumping && wasJumping == false)
		{
			wasJumping = true;

			Transform[] yoffoCubes = GameObject.Find("ActiveLevel").GetComponent<ActiveLevel>().GetAllActiveCubes(CubeType.BLUE);

			for (int i = 0; i < yoffoCubes.Length; i++)
			{
				if (Vector3.Distance(yoffoCubes[i].position, transform.position) < yoffoJumpAlongDistance)
				{
					if (Random.Range(0f, 1f) < yoffoJumpProbability)
					{
						yoffoCubes[i].GetComponent<MoveCube>().Jump();
					}
				}
			}
		}
		else if (fps.Jumping == false && wasJumping)
		{
			wasJumping = false;
		}




















		if (Input.GetMouseButtonUp(1))
        {
			if (nearestCubie != null)
                {
                    Vector3 camFor = Camera.main.transform.forward;
                    camFor.y = 0f;
                    camFor.Normalize();
                    camFor.y = m_kickVecYComp;
                    camFor.Normalize();

				if (nearestCubie.GetComponent<Cubie>().CubeType == CubeType.RED)
				{
					CubieAggro ca = nearestCubie.GetComponent<CubieAggro>();
					nearestCubie.GetComponent<Cubie>().m_navAgent.enabled = false;
					ca.jumpingBack = 1.7f;
				}



				nearestCubie.GetComponent<Rigidbody> ().AddForce (camFor * m_constForce * Mathf.Min (1f, (kick_charge / kickFullChargeTime)));

				if ((kick_charge / kickFullChargeTime) < kick_pushHardBorder) {
					nearestCubie.GetComponent<Cubie> ().Interact (InteractionType.PUSH_SOFT);
				} else {
					Debug.Log ("Butpihs hard");
					nearestCubie.GetComponent<Cubie> ().Interact (InteractionType.PUSH_HARD);
				}


				//Debug.Log("Kicked Cube: " + m_constForce * Mathf.Min(1f, (kick_charge / kickFullChargeTime)));
			}
            
		}


		if (Input.GetMouseButton(1))
		{
			if (nearestCubie != null)
			{
				kick_charge += Time.deltaTime;
			}
		} else if (kick_charge != 0f) {
			kick_charge = 0f;
		}
	}

	private void playDamageSound ()
	{
		int index = Random.Range (0, damageClips.Length);

		audioSourceDamage.clip = damageClips [index];
		audioSourceDamage.Play ();
	}

	public void Die ()
	{
		Debug.Log ("Die");
		GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ().enabled = false;
	
		m_isDead = true;

//		Camera.main.transform.Rotate (Vector3.right * 150);

		Camera.main.backgroundColor = Color.green;
	}

	public int Health {
		get {
			return health;
		}
		set {

			if (value < health) {
				playDamageSound ();
			}

			health = value;
			for (int i = 0; i < health; i++) {
				images_health [i].enabled = true;
			}

			for (int i = health; i < images_health.Length; i++) {
				images_health [i].enabled = false;
			}
		}
	}


	public void PlayScreamAudio ()
	{
		Debug.Log ("O süeal");
		int index = Random.Range (0, selfScreamClips.Length);

		GetComponent<AudioSource> ().clip = selfScreamClips [index];
		GetComponent<AudioSource> ().Play ();
	}
}
