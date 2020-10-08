using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{

    int seed = 1712;
    public GameObject barrelPrefab;

    float offset = (float)8;

    public int barrelSpawnRate = 50;
    
    byte[,] field = new byte[15,15];
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(seed);
        //TODO mapLayoutScript = GameObject.Find("MapLayout").GetComponent<MapLayoutScript>(); 
        //TODO field = mapLayoutScript.field;
        //InitBarrels();
        //PlaceBarrels();
        //mapLayoutScript.LogField(field);
        InitBarrels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitBarrels()
    {
        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 15; x++)
            {
                InitRandomBarrelsInField(x, y);
            }
        }
    }

    void InitRandomBarrelsInField(int x, int y)
    {
        //In the 4 corners nothing should spawn
        if (x <= 3 && y <= 3 || x >= 11 && y <= 3 || x <= 3 && y >= 11 || x >= 11 && y >= 11)
        {
            return;
        }
        else
        {
            if (Random.Range(1, 100) <= barrelSpawnRate)
            {
                PlaceSomethingOnField(x, y, barrelPrefab);
                //TO DO mapLayoutScript.ChangeField(y, x, 2);
                //field[y-1,x-1] = 2;

            }
        }
    }

    public void PlaceBarrels()
    {
        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 15; x++)
            {
                PlaceBarrelsInField(x, y);
            }
        }
    }

    void PlaceBarrelsInField(int x, int y)
    {
       if(field[x,y] == 2)
        {
            PlaceSomethingOnField(x,y,barrelPrefab);
        }
    }

    void PlaceSomethingOnField(int x, int y, GameObject prefab)
    {
        //Instantiate a Prefab (i.e. a wall-tile) on the position x,y on a field
        Instantiate(prefab).transform.position = new Vector3(x - offset+1, (float)0.25, y - offset+1);
    }

}
