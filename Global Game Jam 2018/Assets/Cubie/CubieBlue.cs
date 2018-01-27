using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieBlue : Cubie {

	private Transform m_playerTransform;
	private ActiveLevel m_activeLevel;

	// Use this for initialization
	protected new void Start () {
		base.Start();

		m_playerTransform = GameObject.Find("Player").transform;
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
	}

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
	}

	private float followTime = 0f;
	private bool followingNow = false;

	public void FollowPlayerForSeconds(float seconds)
	{
		followTime = seconds;
		followingNow = true;

		FollowTransform = m_playerTransform;
	}
}
