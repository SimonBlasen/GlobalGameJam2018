using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieAggro : Cubie {

	private Transform m_playerTransform;

	// Use this for initialization
	protected new void Start () {
		base.Start();

		m_playerTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
		if (m_colliderCheckerPlayer.colliderInside.Count > 0)
		{
			FollowTransform = m_playerTransform;
		}
		else
		{
			FollowTransform = null;
		}
	}
}
