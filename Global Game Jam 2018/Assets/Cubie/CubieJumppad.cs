using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieJumppad : Cubie {

	[SerializeField]
	private ColliderChecker colliderPlayerChecker;
	[SerializeField]
	private float jumpPadStrength = 200f;

	protected new void Start () {
		base.Start();

	}


	// Update is called once per frame
	protected new void Update () {
		base.Update();

		if (colliderPlayerChecker.colliderInside.Count > 0)
		{
			colliderPlayerChecker.colliderInside[0].GetComponent<Rigidbody>().velocity = (new Vector3(0f, 4f, -0.3f)) * jumpPadStrength;
			//colliderPlayerChecker.colliderInside[0].GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPadStrength);
		}
	}
}
