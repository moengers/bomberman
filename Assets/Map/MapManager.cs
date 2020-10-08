using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Field with nothing (0), walls (1), barrels(3);
    //private byte[][] field = new byte[15][];
    //field[0] = new byte[15];
    /*
{
    new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
};*/
    //Field with nothing (0), walls (1), barrels(3);
    static byte[,] field = new byte[15, 15];


    float offset = (float)8;

    //1-100
    //  1 low
    //  100 heigt
    //int barrelSpawnRate = 3; 

    //------------------------------------------------Prefabs------------------------------------------------//
    public GameObject planePrefab;
    public GameObject wallPrefab1;
    public GameObject wallPrefab2;
    public GameObject wallPrefab3;
    public GameObject wallPrefab4;
    public GameObject wallPrefab5;
    public GameObject wallPrefab6;
    public GameObject wallPrefab7;
    //public GameObject barrelPrefab;

    //------------------------------------------------Start() and Update()------------------------------------------------//


    // Start is called before the first frame update
    void Start()
    {
        GameObject mapLayout = GameObject.Find("MapLayout");
        MapLayoutS mapLayoutS = mapLayout.GetComponent<MapLayoutS>();
        field = MapLayoutS.initMapfield;
        InitMap();
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }


    //------------------------------------------------Functions------------------------------------------------//

    void InitMap()
    {
        //Init plane
        Instantiate(planePrefab);
        //Init wall and barrels
        InitWallAndBarrels();
    }

    void InitWallAndBarrels()
    {
        int iy = 0;
        int ix = 0;
        

        
        for (int y = 0; y<15; y++)
        {
            
            iy++;
            ix = 0;
            for(int x =0; x<15; x++)
            {
                //Debug.Log("x: " + x + "  y: " + y + "  Field(x,y): " + field[x, y]);
                
                ix++;
                if (field[y, x] == 1)
                {
                    //Debug.Log("Place a Wall Tile");
                    PlaceAWallPiece(iy, ix);
                }
                else
                {
                    //PlaceRandomBarrels(iy, ix);
                }
            }
        }
    }

    void PlaceAWallPiece(int x, int y)
    {
        int r = Random.Range(1, 7);
        switch (r)
        {
            case 1:
                PlaceSomethingOnField(x, y, wallPrefab1);
                break;
            case 2:
                PlaceSomethingOnField(x, y, wallPrefab2);
                break;
            case 3:
                PlaceSomethingOnField(x, y, wallPrefab3);
                break;
            case 4:
                PlaceSomethingOnField(x, y, wallPrefab4);
                break;
            case 5:
                PlaceSomethingOnField(x, y, wallPrefab5);
                break;
            case 6:
                PlaceSomethingOnField(x, y, wallPrefab6);
                break;
            case 7:
                PlaceSomethingOnField(x, y, wallPrefab7);
                break;

        }
        
    }

    /*void PlaceRandomBarrels(int x, int y)
    {
        //In the 4 corners nothing should spawn
        if (x <= 4 && y <= 4 || x >= 12 && y <= 4 || x <= 4 && y >= 12 || x >= 12 && y >= 12)
        {
            return;
        }
        else
        {
            if (Random.Range(1, 100) < barrelSpawnRate)
            {
                PlaceSomethingOnField(x, y, barrelPrefab);
                field[y-1][x-1] = 2;
            }
        }
    }*/

    void PlaceSomethingOnField(int x, int y, GameObject prefab)
    {
        //Instantiate a Prefab (i.e. a wall-tile) on the position x,y on a field
        Instantiate(prefab).transform.position = new Vector3(x - offset, (float)0.25, y - offset);
    }

    


    void LogField(byte[,] field)
    {
        int iy = 0;
        int ix = 0;
        for (int y = 0; y < 15; y++)
        {

            iy++;
            ix = 0;
            for (int x = 0; x < 15; x++)
            {
                Debug.Log("x: " + x + "  y: " + y + "  Field(x,y): " + field[x, y]);
                ix++;
            }
        }
    }

}
