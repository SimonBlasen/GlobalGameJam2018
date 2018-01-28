using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieAggro : Cubie {

	[SerializeField]
	private float jumpBackStrength = 2000f;
	[SerializeField]
	private float yoffoCubesDistance = 5f;

	private ActiveLevel m_activeLevel;

	// Use this for initialization
	protected new void Start () {
		base.Start();
		m_activeLevel = GameObject.Find("ActiveLevel").GetComponent<ActiveLevel>();

	}

	public float jumpingBack = 0f;

	private bool blockedByYoffos = false;
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
		if (m_colliderCheckerPlayer.colliderInside.Count > 0 && jumpingBack <= 0f && blockedByYoffos == false)
		{
			FollowTransform = m_playerTransform;



			//Debug.Log(m_navAgent.velocity.magnitude);
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


		if (m_colliderChecker && m_colliderChecker.colliderInside.Count >= 3)
		{
			int nearYoffoCubes = 0;

			Transform[] allBlues = m_activeLevel.GetAllActiveCubes(CubeType.BLUE);

			for (int i = 0; i < allBlues.Length; i++)
			{
				if (Vector3.Distance(allBlues[i].position, transform.position) < yoffoCubesDistance)
				{
					nearYoffoCubes++;
				}
			}

			//Debug.Log(nearYoffoCubes);

			if (nearYoffoCubes >= 3)
			{
				blockedByYoffos = true;
			}
			else
			{
				blockedByYoffos = false;
			}
		}
		else
		{
			blockedByYoffos = false;
		}


		if (m_colliderClosePlayer && m_colliderClosePlayer.colliderInside.Count > 0 && jumpingBack <= 0f)
		{
			//Debug.Log((transform.forward + (new Vector3(0f, 1f, 0f))).normalized * jumpBackStrength + "," + m_playerTransform.GetComponent<Rigidbody>().mass);
			//m_playerTransform.GetComponent<Rigidbody>().AddForce((transform.forward + (new Vector3(0f, 1f, 0f))).normalized * jumpBackStrength);
			m_playerTransform.GetComponent<Rigidbody>().velocity = (transform.forward + (new Vector3(0f, 1f, 0f))).normalized * jumpBackStrength;
			m_playerTransform.GetComponent<Player>().Health--;

			jumpingBack = 2f;

			m_navAgent.enabled = false;
			//GetComponent<Rigidbody>().AddForce((transform.forward * -1f + new Vector3(0f, 1f, 0f)).normalized * jumpBackStrength);
		}
	}

	public override void Interact(InteractionType interaction)
	{
		if (interaction == InteractionType.SCREAM)
		{
			transform.localScale = transform.localScale * 1.02f;
			SphereCollider[] sphCols = GetComponentsInChildren<SphereCollider>();
			for (int i = 0; i < sphCols.Length; i++)
			{
				if (m_colliderClosePlayer.gameObject.transform != sphCols[i].transform)
				{
					sphCols[i].radius = sphCols[i].radius / 1.02f;
				}
			}
		}
	}
}
