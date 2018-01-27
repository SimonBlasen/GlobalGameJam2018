using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevel : MonoBehaviour
{

	[SerializeField]
	private Transform[] allActiveCubesLevel1;
	private List<Transform> additionalLevel1 = new List<Transform>();
	[SerializeField]
	private Transform[] allActiveCubesLevel2;
	private List<Transform> additionalLevel2 = new List<Transform>();
	[SerializeField]
	private Transform[] allActiveCubesLevel3;
	private List<Transform> additionalLevel3 = new List<Transform>();
	[SerializeField]
	private Transform[] allActiveCubesLevel4;
	private List<Transform> additionalLevel4 = new List<Transform>();
	[SerializeField]
	private Transform[] allActiveCubesLevel5;
	private List<Transform> additionalLevel5 = new List<Transform>();

	private int activeLevel = 0;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	public Transform[] GetAllActiveCubes(CubeType cubeType)
	{
		List<Transform> accCubes = new List<Transform>();
		switch (activeLevel)
		{
		case 0:

			for (int i = 0; i < allActiveCubesLevel1.Length; i++)
			{
				if (allActiveCubesLevel1[i] && allActiveCubesLevel1[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(allActiveCubesLevel1[i]);
				}
			}

			for (int i = 0; i < additionalLevel1.Count; i++)
			{
				if (additionalLevel1[i] && additionalLevel1[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(additionalLevel1[i]);
				}
			}


			return accCubes.ToArray();
		case 1:


			for (int i = 0; i < allActiveCubesLevel2.Length; i++)
			{
				if (allActiveCubesLevel2[i] && allActiveCubesLevel2[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(allActiveCubesLevel2[i]);
				}
			}

			for (int i = 0; i < additionalLevel2.Count; i++)
			{
				if (additionalLevel2[i] && additionalLevel2[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(additionalLevel2[i]);
				}
			}


			return accCubes.ToArray();
		case 2:


			for (int i = 0; i < allActiveCubesLevel3.Length; i++)
			{
				if (allActiveCubesLevel3[i] && allActiveCubesLevel3[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(allActiveCubesLevel3[i]);
				}
			}

			for (int i = 0; i < additionalLevel3.Count; i++)
			{
				if (additionalLevel3[i] && additionalLevel3[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(additionalLevel3[i]);
				}
			}


			return accCubes.ToArray();
		case 3:


			for (int i = 0; i < allActiveCubesLevel4.Length; i++)
			{
				if (allActiveCubesLevel4[i] && allActiveCubesLevel4[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(allActiveCubesLevel4[i]);
				}
			}

			for (int i = 0; i < additionalLevel4.Count; i++)
			{
				if (additionalLevel4[i] && additionalLevel4[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(additionalLevel4[i]);
				}
			}


			return accCubes.ToArray();
		case 4:


			for (int i = 0; i < allActiveCubesLevel5.Length; i++)
			{
				if (allActiveCubesLevel5[i] && allActiveCubesLevel5[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(allActiveCubesLevel5[i]);
				}
			}

			for (int i = 0; i < additionalLevel5.Count; i++)
			{
				if (additionalLevel5[i] && additionalLevel5[i].GetComponent<Cubie>().CubeType == cubeType)
				{
					accCubes.Add(additionalLevel5[i]);
				}
			}


			return accCubes.ToArray();
		}

		return null;
	}

	public void AddActiveCube(Transform cube)
	{
		switch (activeLevel)
		{
		case 0:
			additionalLevel1.Add(cube);
			break;
		case 1:
			additionalLevel2.Add(cube);
			break;
		case 2:
			additionalLevel3.Add(cube);
			break;
		case 3:
			additionalLevel4.Add(cube);
			break;
		case 4:
			additionalLevel5.Add(cube);
			break;
		}
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
