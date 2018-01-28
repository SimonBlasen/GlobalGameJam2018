using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CubieGreen : Cubie {

	[SerializeField]
	private AudioClip[] screamClips;

	protected new void Start () {
		base.Start();

	}

	private bool runningAway = false;
	private float oldNavSpeed = 0f;

	// Update is called once per frame
	protected new void Update () {
		base.Update();


		if (m_colliderCheckerPlayer.colliderInside.Count > 0)
		{
			if (m_colliderCheckerPlayer.colliderInside[0].GetComponent<RigidbodyFirstPersonController>().Running)
			{
				wanderAllowed = false;
				m_navAgent.enabled = true;
				oldNavSpeed = m_navAgent.speed;
				m_navAgent.speed = 8f;
				m_navAgent.destination = GameObject.Find("RandomWalkSpawns").GetComponent<RandomMovementSpawns>().spawns[Random.Range(0, GameObject.Find("RandomWalkSpawns").GetComponent<RandomMovementSpawns>().spawns.Length)].position;
			
				runningAway = true;
			}
		}

		if (runningAway && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(m_navAgent.destination.x, m_navAgent.destination.z)) < 1f)
		{
			runningAway = false;
			FollowTransform = null;

			m_navAgent.speed = oldNavSpeed;
		}
	}


	public override void Interact(InteractionType interaction)
	{
		if (interaction == InteractionType.SCREAM)
		{
			Debug.Log("Green heared");
			Invoke("ScreamSelf", Random.Range(0.5f, 1.6f));
		}
	}

	public void ScreamSelf()
	{
		int index = Random.Range(0, screamClips.Length);

		PlayAudio(screamClips[index]);
	}
}
