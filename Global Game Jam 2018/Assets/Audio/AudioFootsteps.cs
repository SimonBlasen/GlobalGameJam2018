using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AudioFootsteps : MonoBehaviour {

	[SerializeField]
	private AudioClip[] footsteps;
	[SerializeField]
	private float stepsSpeed = 1f;
	[SerializeField]
	private RigidbodyFirstPersonController fps;

	private float stepsCounter = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (WalkingSpeed > 0.1f && fps.Grounded)
		{
			stepsCounter += Time.deltaTime * WalkingSpeed;
			if (stepsCounter >= stepsSpeed)
			{
				stepsCounter = 0f;

				playStepSound();
			}
		}

	}

	public float WalkingSpeed { get; set ; }

	private void playStepSound()
	{
		int soundIndex = Random.Range(0, footsteps.Length);

		GetComponent<AudioSource>().clip = footsteps[soundIndex];
		GetComponent<AudioSource>().Play();
	}
}
