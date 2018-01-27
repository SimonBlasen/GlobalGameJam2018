using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderChecker : MonoBehaviour {

	[SerializeField]
	private string objectsToTag = "";

	[HideInInspector]
    public List<Transform> colliderInside = new List<Transform>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == objectsToTag)
        {
            colliderInside.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.tag == objectsToTag)
        {
            colliderInside.Remove(other.transform);
        }
    }


}
