using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerScript : MonoBehaviour {

	public RawImage rawImage;
	public RawImage rawImageBehind;

	public VideoClip intorClip;

	public VideoClip intorClip2;
	public VideoSource source;

	public VideoPlayer player;
	public VideoPlayer playerBehind;
	public MovieTexture mt;

	// Use this for initialization
	void Start () {

		rawImageBehind.enabled = false;

		StartCoroutine(playVide());


	}
	
	// Update is called once per frame
	void Update () {



		if (finished1 && started2 == false)
		{
			//player.Play();
			started2 = true;
			//StartCoroutine(playVide11());#

			rawImage.enabled = false;
			rawImageBehind.enabled 
			= true;
			Debug.Log("Play 2");
			playerBehind.Play();
		}

		//if (finished2 && started2 && complFinished == false)
		//{
		//	complFinished = true;

		//}
	}

	private bool finished1 = false;
	private bool finished2 = false;
	private bool started2 = false;
	private bool complFinished = false;


	IEnumerator playVide(){

		playerBehind.playOnAwake = false;
		player.playOnAwake = false;
		player.clip = intorClip;
		playerBehind.clip = intorClip2;

		player.Prepare();
		playerBehind.Prepare();

		while ((!player.isPrepared) && (!playerBehind.isPrepared))
		{
			yield return null;
		}

		rawImage.texture = player.texture;
		rawImageBehind.texture = playerBehind.texture;

		player.Play();

		while (player.isPlaying)
		{
			yield return null;
		}



		player.Stop();
		finished1 = true;

	}
}
