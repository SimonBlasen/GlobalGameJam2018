using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieGreen : Cubie {

	[SerializeField]
	private AudioClip[] screamClips;

	protected new void Start () {
		base.Start();

	}


	// Update is called once per frame
	protected new void Update () {
		base.Update();

	}


	public override void Interact(InteractionType interaction)
	{
		if (interaction == InteractionType.SCREAM)
		{
			Debug.Log("Green heared");
			Invoke("ScreamSelf", Random.Range(0f, 2f));
		}
	}

	public void ScreamSelf()
	{
		int index = Random.Range(0, screamClips.Length);

		PlayAudio(screamClips[index]);
	}
}
