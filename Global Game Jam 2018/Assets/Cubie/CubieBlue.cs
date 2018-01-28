using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CubieBlue : Cubie {

	[SerializeField]
	private AudioClip[] yoffoSounds;

	private ActiveLevel m_activeLevel;

	// Use this for initialization
	protected new void Start () {
		base.Start();

		m_activeLevel = GameObject.Find("ActiveLevel").GetComponent<ActiveLevel>();
	}

	// Update is called once per frame
	protected new void Update () {
		base.Update();

		if (followingNow)
		{
			followTime -= Time.deltaTime;

			if (followTime <= 0f)
			{
				FollowTransform = null;
				followingNow = false;
			}

		}



		if (runningAway && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(m_navAgent.destination.x, m_navAgent.destination.z)) < 1f)
		{
			runningAway = false;
			FollowTransform = null;

			m_navAgent.speed = oldNavSpeed;
		}

	}


	private bool runningAway = false;
	private float oldNavSpeed= 0f;

	public override void Interact(InteractionType interaction)
	{
		if (interaction == InteractionType.PUSH_HARD)
		{
			Debug.Log("Blue pushed hard");

			Transform[] allBlues = m_activeLevel.GetAllActiveCubes(CubeType);

			for (int i = 0; i < allBlues.Length; i++)
			{
				if (allBlues[i] != transform)
				{
					allBlues[i].GetComponent<CubieBlue>().FollowPlayerForSeconds(2f);
				}
			}
		}
		else if (interaction == InteractionType.SCREAM)
		{
			wanderAllowed = false;
			m_navAgent.enabled = true;
			oldNavSpeed = m_navAgent.speed;
			m_navAgent.speed = 8f;
			m_navAgent.destination = GameObject.Find("RandomWalkSpawns").GetComponent<RandomMovementSpawns>().spawns[Random.Range(0, GameObject.Find("RandomWalkSpawns").GetComponent<RandomMovementSpawns>().spawns.Length)].position;

			runningAway = true;
		}
	}

	private float followTime = 0f;
	private bool followingNow = false;

	public void FollowPlayerForSeconds(float seconds)
	{
		Invoke("PlayYoffoSound", Random.Range(0f, 1f));

		followTime = seconds;
		followingNow = true;

		FollowTransform = m_playerTransform;
	}

	private void PlayYoffoSound()
	{
		PlayAudio(yoffoSounds[Random.Range(0, yoffoSounds.Length)]);
	}
}
