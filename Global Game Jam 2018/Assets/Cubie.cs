using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubie : MonoBehaviour {

    [SerializeField]
    private ColliderChecker colliderChecker;
    [SerializeField]
    private CubeType type;

    private PowercubeCreator powercubeCreator;

    // Use this for initialization
    void Start ()
    {
        powercubeCreator = GameObject.Find("PowercubeCreator").GetComponent<PowercubeCreator>();
	}
	
	// Update is called once per frame
	void Update ()
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
	}

    public CubeType CubeType
    {
        get
        {
            return type;
        }
    }
}
