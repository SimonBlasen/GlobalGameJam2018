using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cubie : MonoBehaviour {

    [SerializeField]
	protected ColliderChecker colliderChecker;
	[SerializeField]
	protected ColliderChecker colliderCheckerPlayer;
    [SerializeField]
    private CubeType type;

    protected PowercubeCreator powercubeCreator;
	protected NavMeshAgent navAgent;

    // Use this for initialization
    protected void Start ()
    {
        powercubeCreator = GameObject.Find("PowercubeCreator").GetComponent<PowercubeCreator>();
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.enabled = false;
	}
	
	// Update is called once per frame
	protected void Update ()
    {
		if (colliderChecker.colliderInside.Count > 0)
        {
            //Debug.Log("Other cubes inside: " + colliderChecker.colliderInside.Count);

            Transform[] transCubes = new Transform[colliderChecker.colliderInside.Count + 1];
            for (int i = 0; i < colliderChecker.colliderInside.Count; i++)
            {
                transCubes[i + 1] = colliderChecker.colliderInside[i];
            }
            transCubes[0] = transform;

            powercubeCreator.CheckforPowercube(transCubes);


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
			if (navAgent == null)
			{
				navAgent = GetComponent<NavMeshAgent>();
			}

			if (navAgent.isOnNavMesh)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				navAgent.destination = followTrans.position;
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
				navAgent.enabled = true;
			}
			else
			{
				navAgent.enabled = false;
			}
		}
	}
		
    public CubeType CubeType
    {
        get
        {
            return type;
        }
    }
}
