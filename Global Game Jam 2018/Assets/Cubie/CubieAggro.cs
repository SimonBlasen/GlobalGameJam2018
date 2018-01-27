using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieAggro : Cubie {

	[SerializeField]
	private float jumpBackStrength = 2000f;

	private Transform m_playerTransform;

	// Use this for initialization
	protected new void Start () {
		base.Start();

		m_playerTransform = GameObject.Find("Player").transform;
	}

	private float jumpingBack = 0f;
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
		if (m_colliderCheckerPlayer.colliderInside.Count > 0 && jumpingBack <= 0f)
		{
			FollowTransform = m_playerTransform;
		}
		else
		{
			FollowTransform = null;
		}



		if (jumpingBack > 0f)
		{
			jumpingBack -= Time.deltaTime;

			if (jumpingBack <= 0f)
			{
				m_navAgent.enabled = true;
			}
		}



		if (m_colliderClosePlayer && m_colliderClosePlayer.colliderInside.Count > 0 && jumpingBack <= 0f)
		{
			m_playerTransform.GetComponent<Player>().Health--;

			jumpingBack = 2f;

			m_navAgent.enabled = false;
			GetComponent<Rigidbody>().AddForce((transform.forward * -1f + new Vector3(0f, 1f, 0f)).normalized * jumpBackStrength);
		}
	}
}
