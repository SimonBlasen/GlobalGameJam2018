using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowercubeCreator : MonoBehaviour {

    [SerializeField]
    private CubeConstellation[] constellations
        = new CubeConstellation[]
        {
		new CubeConstellation(new CubeType[] { CubeType.RED, CubeType.GREEN }, CubeType.YELLOW),
		new CubeConstellation(new CubeType[] { CubeType.RED, CubeType.RED }, CubeType.GREEN),

        };

    [Header("References")]
    [SerializeField]
    private AllCubes allCubes;




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckforPowercube(Transform[] cubes)
    {
        if (cubes.Length >= 2)
        {
            for (int cc = 0; cc < constellations.Length; cc++)
            {
                bool matches = constellations[cc].Matches(cubes);

                if (matches)
                {
					Debug.Log("Constellation [" + cc + "] matches");
                    CubeType resultType = constellations[cc].resultType;

                    GameObject instCube = Instantiate(allCubes.cubePrefabs[(int)resultType]);
                    instCube.transform.position = middleValue(cubes);

                    for (int i = 0; i < cubes.Length; i++)
                    {
                        Destroy(cubes[i].gameObject);
                    }
                }
            }
        }
    }

    private static Vector3 middleValue(Transform[] transforms)
    {
        Vector3 mid = Vector3.zero;

        for (int i = 0; i < transforms.Length; i++)
        {
            mid += transforms[i].position;
        }

        return mid / ((float)transforms.Length);
    }
}



public class CubeConstellation
{
    public CubeType[] cubeTypes;
    public CubeType resultType;

    public CubeConstellation(CubeType[] types, CubeType result)
    {
        cubeTypes = types;
        resultType = result;
    }

    public bool Matches(Transform[] cubes)
    {
        if (cubes.Length != cubeTypes.Length)
        {
            return false;
        }

        List<CubeType> tTypes = new List<CubeType>();
        for (int i = 0; i < cubeTypes.Length; i++)
        {
            tTypes.Add(cubeTypes[i]);
        }

		for (int i = 0; i < cubes.Length; i++)
        {
			tTypes.Remove(cubes[i].GetComponent<Cubie>().CubeType);
            //tTypes.Remove(cubeTypes[i]);
        }

        return tTypes.Count == 0;
    }
}