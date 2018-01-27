using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cubie : MonoBehaviour {

    [SerializeField]
	protected ColliderChecker m_colliderChecker;
	[SerializeField]
	protected ColliderChecker m_colliderCheckerPlayer;
    protected PowercubeCreator m_powercubeCreator;
	protected NavMeshAgent m_navAgent;
    [SerializeField]
    private CubeType m_type;

    [SerializeField]
    private bool m_isAttacking;


    

    // Use this for initialization
    protected void Start ()
    {
        m_powercubeCreator = GameObject.Find("PowercubeCreator").GetComponent<PowercubeCreator>();
	    m_navAgent = GetComponent<NavMeshAgent>();
		m_navAgent.enabled = false;
        LoadColliderChecker();
        
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
	
	// Update is called once per frame
	protected void Update ()
    {
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


	public virtual void Interact(InteractionType interaction)
	{

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
				m_navAgent.enabled = true;
			}
			else
			{
				m_navAgent.enabled = false;
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
