using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevel : MonoBehaviour {

	[SerializeField]
	private Transform[] allActiveCubesLevel1;
	[SerializeField]
	private Transform[] allActiveCubesLevel2;
	[SerializeField]
	private Transform[] allActiveCubesLevel3;
	[SerializeField]
	private Transform[] allActiveCubesLevel4;
	[SerializeField]
	private Transform[] allActiveCubesLevel5;

	private int activeLevel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Transform[] GetAllActiveCubes(CubeType cubeType)
	{
		List<Transform> accCubes = new List<Transform>();
		switch (activeLevel)
		{
		case 0:

			for (int i = 0; i < allActiveCubesLevel1.Length; i++)
			{
				if (allActiveCubesLevel1[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(allActiveCubesLevel1[i]);
				}
			}


						return accCubes.ToArray();
		case 1:


						for (int i = 0; i < allActiveCubesLevel2.Length; i++)
						{
							if (allActiveCubesLevel2[i].GetComponent<Cubie>().CubeType == cubeType)
							{
					accCubes.Add(allActiveCubesLevel2[i]);
									}
									}


									return accCubes.ToArray();
		case 2:


			for (int i = 0; i < allActiveCubesLevel3.Length; i++)
						{
				if (allActiveCubesLevel3[i].GetComponent<Cubie>().CubeType == cubeType)
							{
					accCubes.Add(allActiveCubesLevel3[i]);
									}
									}


									return accCubes.ToArray();
		case 3:


						for (int i = 0; i < allActiveCubesLevel4.Length; i++)
						{
				if (allActiveCubesLevel4[i].GetComponent<Cubie>().CubeType == cubeType)
							{
					accCubes.Add(allActiveCubesLevel4[i]);
									}
									}


									return accCubes.ToArray();
		case 4:


						for (int i = 0; i < allActiveCubesLevel5.Length; i++)
						{
				if (allActiveCubesLevel5[i].GetComponent<Cubie>().CubeType == cubeType)
							{
					accCubes.Add(allActiveCubesLevel5[i]);
									}
									}


									return accCubes.ToArray();
		}

		return null;
	}

	public Transform[] GetAllActiveCubes()
	{
		switch (activeLevel)
		{
		case 0:

			return allActiveCubesLevel1;
		case 1:

			return allActiveCubesLevel2;
		case 2:

			return allActiveCubesLevel3;
		case 3:

			return allActiveCubesLevel4;
		case 4:

			return allActiveCubesLevel5;
		}


		return null;
	}
}
