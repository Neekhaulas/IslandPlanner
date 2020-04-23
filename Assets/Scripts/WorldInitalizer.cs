using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInitalizer : MonoBehaviour
{
    public GameObject Grass;
    public GameObject Water;
    public World World;

    // Start is called before the first frame update
    void Start()
    {
        World = FindObjectOfType<World>();
        for (int y = 0; y < 1; y++)
        {
            for (int z = 0; z < 15 * 5; z++)
            {
                for (int x = 0; x < 15 * 6; x++)
                {
                    switch(World.Map[x,z,y])
                    {
                        case 0:
                            if(y != 0) Instantiate(Water, new Vector3(x, -0.3f, z), Quaternion.identity);
                            break;

                        case 1:
                            Instantiate(Grass, new Vector3(x, 0, z), Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
