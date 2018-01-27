using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepySoundsSpawner : MonoBehaviour {

	[SerializeField]
	private AudioClip[] clips;
	[SerializeField]
	private GameObject prefabSound;
	[SerializeField]
	private int parallelAmounts = 3;
	[SerializeField]
	private float maxRandValue = 10f;

	private List<float> cds =new List<float>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < parallelAmounts; i++)
		{
			cds.Add(0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cds.Count; i++)
		{
			cds[i] -= Time.deltaTime;

			if (cds[i] <= 0f)
			{
				cds[i] = Random.Range(0f, maxRandValue);

				GameObject instSound = Instantiate(prefabSound);
			
				int index = Random.Range(0, clips.Length);

				float randomAngle = Random.Range(0f, Mathf.PI * 2f);
				instSound.transform.position = GameObject.Find("Player").transform.position + ((new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle))) * 5f);

				instSound.GetComponent<AudioSource>().clip = clips[index];
				instSound.GetComponent<AudioSource>().Play();

				Destroy(instSound, 15f);
			}
		}
	}
}
