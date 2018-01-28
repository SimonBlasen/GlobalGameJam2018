using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerScript : MonoBehaviour {

	public RawImage rawImage;

	public VideoClip intorClip;

	public VideoSource source;

	public VideoPlayer player;

	// Use this for initialization
	void Start () {
		player.clip = intorClip;


		rawImage.texture = player.texture;

		player.Prepare();

	}
	
	// Update is called once per frame
	void Update () {
		if (player.isPlaying == false && player.isPrepared)
		{
			player.Play();
		}
	}
}
