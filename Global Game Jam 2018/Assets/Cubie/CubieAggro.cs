using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieAggro : Cubie {

	private Transform player;

	// Use this for initialization
	protected new void Start () {
		base.Start();

		player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();

		if (colliderCheckerPlayer.colliderInside.Count > 0)
		{
			FollowTransform = player;
		}
		else
		{
			FollowTransform = null;
		}
	}
}
