using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cubie : MonoBehaviour {

	[SerializeField]
	private AudioClip clipMoving;
	[SerializeField]
	protected ColliderChecker m_colliderClosePlayer;
    [SerializeField]
	protected ColliderChecker m_colliderChecker;
	[SerializeField]
	protected ColliderChecker m_colliderCheckerPlayer;
    protected PowercubeCreator m_powercubeCreator;
	public NavMeshAgent m_navAgent;
    [SerializeField]
    private CubeType m_type;
	[SerializeField]
	private float move_random_prob_min = 1000f;
	[SerializeField]
	private float move_random_prob_max = 1001f;
	[SerializeField]
	private float move_random_duration_min = 0f;
	[SerializeField]
	private float move_random_duration_max = 0.1f;

    [SerializeField]
    private bool m_isAttacking;

	[SerializeField]
	protected AudioSource sourceMoving;

	protected Transform m_playerTransform;
	protected AudioSource m_audioSource;


	public bool wanderAllowed = false;
	private bool origWnaderAllowed = false;
	protected bool currently_wandering = false;
    

    // Use this for initialization
    protected void Start ()
    {
		origWnaderAllowed = wanderAllowed;
		m_powercubeCreator = GameObject.Find("PowercubeCreator").GetComponent<PowercubeCreator>();
		m_playerTransform = GameObject.Find("Player").transform;
	    m_navAgent = GetComponent<NavMeshAgent>();
		m_navAgent.enabled = false;
        //LoadColliderChecker();
		m_audioSource = GetComponent<AudioSource>();
        
	}

    private void LoadColliderChecker()
    {
        foreach(ColliderChecker checker in GetComponentsInChildren<ColliderChecker>())
        {
            switch(checker.getObjectsToTag())
            {
                case "Cube":
                    m_colliderChecker = checker;
                break;
                case "Player":
                    m_colliderCheckerPlayer = checker;
                break;
                default: break;
            }
        }
    }

	private bool lookAtPlayer = false;

	private float wanderTimeWait = 0f;
	private float wanderTime = 0f;
	private Transform wander_target = null;

	private int wanderState = 0;

	// Update is called once per frame
	protected void Update ()
    {
		if (wanderAllowed && wanderState == 0)
		{
			wanderTimeWait = Random.Range(move_random_prob_min, move_random_prob_max);
			currently_wandering = true;
			wander_target = null;

			wanderState = 1;

		}

		if (wanderAllowed && currently_wandering && wanderTimeWait > 0f && wanderState == 1)
		{
			wanderTimeWait -= Time.deltaTime;
		}

		if (wanderAllowed && currently_wandering && wanderTimeWait <= 0f && wander_target == null && wanderState ==1)
		{
			wanderState = 2;
			wander_target = GameObject.Find("RandomWalkSpawns").GetComponent<RandomMovementSpawns>().spawns[Random.Range(0, GameObject.Find("RandomWalkSpawns").GetComponent<RandomMovementSpawns>().spawns.Length)];
		}


		if (wanderAllowed && currently_wandering && wanderTimeWait <= 0f && wander_target != null && m_navAgent.enabled == false && wanderState == 2)
		{
			m_navAgent.enabled = true;

			m_navAgent.destination = wander_target.position;

			wanderTime = Random.Range(move_random_duration_min, move_random_duration_max);

			wanderState = 3;
		}

		if (wanderAllowed && currently_wandering && wanderTimeWait <= 0f && wander_target != null && wanderTime > 0f && m_navAgent.enabled && wanderState == 3)
		{
			wanderTime -= Time.deltaTime;
		
		
		}


		if (wanderAllowed && currently_wandering && wanderTimeWait <= 0f && wander_target != null && wanderTime <= 0f && m_navAgent.enabled && wanderState ==3)
		{
			wanderState 
			= 0;
			currently_wandering = false;
			m_navAgent.enabled = false;
		}

		if (wanderAllowed == false)
		{
			currently_wandering = false;
			wander_target = null;
		}



		RaycastHit toGround;

		if (m_navAgent.velocity.magnitude > 0.1f)
		{
				if (sourceMoving.isPlaying == false)
				{
					sourceMoving.clip = clipMoving;
					sourceMoving.loop = true;
					sourceMoving.Play();
				}
		}
		else
		{
			sourceMoving.Stop();
		}

		if (lookAtPlayer && m_navAgent.enabled == false)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_playerTransform.position - transform.position), 0.2f);

			if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_playerTransform.position - transform.position)) < 3f)
			{
				lookAtPlayer = false;
			}
		}

		if (m_colliderCheckerPlayer &&m_colliderCheckerPlayer.colliderInside.Count > 0)
        {
            //Debug.Log("Other cubes inside: " + m_colliderChecker.colliderInside.Count);

            Transform[] transCubes = new Transform[m_colliderChecker.colliderInside.Count + 1];
            for (int i = 0; i < m_colliderChecker.colliderInside.Count; i++)
            {
                transCubes[i + 1] = m_colliderChecker.colliderInside[i];
            }
            transCubes[0] = transform;

            m_powercubeCreator.CheckforPowercube(transCubes);


            //Material mat = new Material(GetComponent<MeshRenderer>().material);
            //mat.color = Color.yellow;
            //GetComponent<MeshRenderer>().material = mat;
        }
        else
        {
            //Material mat = new Material(GetComponent<MeshRenderer>().material);
            //mat.color = Color.red;
            //GetComponent<MeshRenderer>().material = mat;
        }


		if (FollowTransform != null)
		{
			if (m_navAgent == null)
			{
				m_navAgent = GetComponent<NavMeshAgent>();
			}

			if (m_navAgent.isOnNavMesh)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				m_navAgent.destination = followTrans.position;
			}	
		}
	}

	public void PlayAudio(AudioClip clip)
	{
		m_audioSource.clip = clip;
		m_audioSource.Play();

		lookAtPlayer = true;
	}



	public virtual void Interact(InteractionType interaction)
	{
		Debug.Log("Parent is executed");
	}

	protected Transform followTrans = null;

	public Transform FollowTransform {
		get {
			return followTrans;
		}
		set {
			followTrans = value;

			if (followTrans != null)
			{
				wanderAllowed = false;
				m_navAgent.enabled = true;
			}
			else
			{
				if (wanderAllowed == false)
				{
					m_navAgent.enabled = false;
				}

				if (origWnaderAllowed)
				{
					wanderAllowed = true;
				}
				//m_navAgent.enabled = false;
			}
		}
	}
		
    public CubeType CubeType
    {
        get
        {
            return m_type;
        }
    }



	
}
