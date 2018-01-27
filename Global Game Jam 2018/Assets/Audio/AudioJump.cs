using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;





public class AudioJump : MonoBehaviour {

	[SerializeField]
	private AudioClip jumpUp;
	[SerializeField]
	private AudioClip jumpLand;
	[SerializeField]
	private RigidbodyFirstPersonController fps;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (fps.Jumping && wasJumping == false)
		{
			wasJumping = true;

			PlayJump();
		}
		else if (fps.Jumping == false && wasJumping)
		{
			wasJumping = false;

			PlayJumpLand();
		}
	}

	private bool wasJumping = false;

	public void PlayJump()
	{
		GetComponent<AudioSource>().clip = jumpUp;
		GetComponent<AudioSource>().Play();
	}

	public void PlayJumpLand()
	{
		GetComponent<AudioSource>().clip = jumpLand;
		GetComponent<AudioSource>().Play();
	}
}