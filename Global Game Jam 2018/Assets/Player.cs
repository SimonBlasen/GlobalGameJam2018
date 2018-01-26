using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

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

	[Header("References")]
	[SerializeField]
	private Image image_crosshairOutofrange;
	[SerializeField]
	private Image image_crosshairInrange;
	[SerializeField]
	private Image image_kickCharge;


    private Transform m_grabbedCubem = null;
    private Vector3 m_grab_relative = Vector3.zero;

	private float kick_charge = 0f;
	private float imageKickChargeX = 0f;
	private float imageKickChargeY = 0f;


	// Use this for initialization
	void Start () {
        Cursor.visible = true;

		imageKickChargeX = image_kickCharge.GetComponent<RectTransform>().sizeDelta.x;
		imageKickChargeY = image_kickCharge.GetComponent<RectTransform>().sizeDelta.y;
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (kick_charge != 0f)
		{
			image_kickCharge.GetComponent<RectTransform>().sizeDelta = new Vector2(imageKickChargeX * Mathf.Min(1f, (kick_charge / kickFullChargeTime)), imageKickChargeY);
		}
		else if (image_kickCharge.GetComponent<RectTransform>().sizeDelta.x != 0f)
		{
			image_kickCharge.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, imageKickChargeY);
		}
			

		RaycastHit[] hitsCrosshair = Physics.RaycastAll(new Ray(Camera.main.transform.position, Camera.main.transform.forward), m_hoverRadius);
		Transform nearestCubie = null;
		if (hitsCrosshair.Length > 0)
		{
			float nearestDistance = float.MaxValue;

			for (int i = 0; i < hitsCrosshair.Length; i++)
			{
				if (hitsCrosshair[i].transform.GetComponent<Cubie>() != null)
				{
					if (hitsCrosshair[i].distance < nearestDistance)
					{
						nearestDistance = hitsCrosshair[i].distance;
						nearestCubie = hitsCrosshair[i].transform;
					}
				}
			}

			if (nearestCubie != null)
			{
				image_crosshairInrange.enabled = true;
				image_crosshairOutofrange.enabled = false;
			}
			else
			{
				image_crosshairInrange.enabled = false;
				image_crosshairOutofrange.enabled = true;
			}
		}
		else
		{
			image_crosshairInrange.enabled = false;
			image_crosshairOutofrange.enabled = true;
		}






        if (Input.GetMouseButtonDown(0))
        {
				if (nearestCubie != null)
                {
				m_grabbedCubem = nearestCubie;
				m_grab_relative = nearestCubie.position - transform.position;
                }
            
        }

        if (Input.GetMouseButtonUp(0) && m_grabbedCubem != null)
        {
            m_grabbedCubem = null;
        }


        if (m_grabbedCubem)
        {
            m_grabbedCubem.position = transform.position + Camera.main.transform.forward * m_hoverRadius;
			m_grabbedCubem.GetComponent<Rigidbody>().velocity = Vector3.zero;
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


				nearestCubie.GetComponent<Rigidbody>().AddForce(camFor * m_constForce * Mathf.Min(1f, (kick_charge / kickFullChargeTime)));

				if ((kick_charge / kickFullChargeTime) < kick_pushHardBorder)
				{
					nearestCubie.GetComponent<Cubie>().Interact(InteractionType.PUSH_SOFT);
				}
				else
				{
					nearestCubie.GetComponent<Cubie>().Interact(InteractionType.PUSH_HARD);
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
		}
		else if (kick_charge != 0f)
		{
			kick_charge = 0f;
		}
    }
}
