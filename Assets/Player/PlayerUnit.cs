using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class PlayerUnit : NetworkBehaviour
{
    //Child: Prefab
    Transform childPrefabtransform;

    Quaternion lookingRotation;

    public GameObject bombprefab;
    public Vector3 position;
    public int explotionRang = 2;
    public int placerPLayer = 0;
    //Every Player can only place one bomb at the same time
    private int placedBombs = 0;
    public int maxBombPlacings = 1;

    BarrelController barrelController;
    BombPlacer bombPlacerScript;

    //Health
    public int health = 3;

    //WIN and Lost Screen PanelWin
    GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        //barrelController = GameObject.Find("BarrelController").GetComponent<BarrelController>();
        bombPlacerScript = GameObject.FindGameObjectsWithTag("BombPlacer")[0].GetComponent<BombPlacer>();
        //placedBombs = 0;
        childPrefabtransform = this.transform.GetChild(0);
        lookingRotation = Quaternion.Euler(0, 0, 0);

        /*
        if (hasAuthority)
        {
            Debug.Log("Was here 1");

            panelWin = GameObject.Find("PanelWin");
            panelLost = GameObject.Find("PanelLost");
            panelWin.SetActive(false);
            panelLost.SetActive(false);
        }*/
        canvas = GameObject.Find("Canvas");

        if (/*hasAuthority*/true)
        {
            //CmdSpawnBarrels();
        }
    }

    Vector3 velocity;
    Vector3 bestGeuessPosition;

     
    float ourLatancy;
    float latencySmoothingFactor = (float) 0.01;
    
    float x;
    float y;
    float z;
    

    //Looking Directions
    Quaternion lookUp = Quaternion.Euler(0, 0, 0);
    Quaternion lookUpRight = Quaternion.Euler(0, 45, 0);
    Quaternion lookRight = Quaternion.Euler(0, 90, 0);
    Quaternion lookDownRight = Quaternion.Euler(0, 135, 0);
    Quaternion lookDown = Quaternion.Euler(0, 180, 0);
    Quaternion lookDownLeft = Quaternion.Euler(0, 225, 0);
    Quaternion lookLeft = Quaternion.Euler(0, 270, 0);
    Quaternion lookUpLeft = Quaternion.Euler(0, 315, 0);
    

    public float speed;

    // Update is called once per frame
    void Update()
    {

        if (!hasAuthority)
        {
            bestGeuessPosition = bestGeuessPosition + (velocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, bestGeuessPosition, Time.deltaTime * latencySmoothingFactor);

            childPrefabtransform.transform.rotation = lookingRotation;

            return;
        }

        //hasAuthority == true:
        
        //Movement:
        x = 0;
        y = 0;
        z = 0;

        //Right & Left
        x = Input.GetAxis("Horizontal") * speed;
        //Up & Down
        z = Input.GetAxis("Vertical") * speed;

        transform.Translate(velocity * Time.deltaTime);      
        velocity = new Vector3(x, y, z);
        CmdUpdateVelocity(velocity, transform.position);

        lookingRotation = LookingInRightDirection(x, z);
        childPrefabtransform.transform.rotation = lookingRotation;
        CmdUpdateRotation(lookingRotation);

        //Place a Bomb
        if (placedBombs < maxBombPlacings)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                placedBombs++;
                PlaceABomb();
                StartCoroutine(BombPlaceCountDownCoroutine());
            }
        }
        
        
    }


    //Change Vekicuity and Position
    [Command]
    void CmdUpdateVelocity(Vector3 v, Vector3 p)
    {
        transform.position = p;
        velocity = v;
        RpcUpdateVelocity(v, p);
    }

    [ClientRpc]
    void RpcUpdateVelocity(Vector3 v, Vector3 p)
    {
        if (hasAuthority)
        {
            return;
        }
        velocity = v;
        bestGeuessPosition = p + (velocity * (ourLatancy));

    }

    //Change the Rotation
    [Command]
    void CmdUpdateRotation(Quaternion r)
    {
        lookingRotation = r;
        childPrefabtransform.transform.rotation = r;
        RpcUpdateRotation(r);
    }
    [ClientRpc]
    void RpcUpdateRotation(Quaternion r)
    {
        if (hasAuthority)
        {
            return;
        }
        lookingRotation = r;
        childPrefabtransform.transform.rotation = r;

    }

    //Place a Bomb
    void PlaceABomb()
    {
        position = V3Rounder(this.transform.position);
        //Instantiate(bombprefab);
        bombPlacerScript.PlaceABomb(position);
        CmdPlaceABomb(position);
    }
    [Command]
    void CmdPlaceABomb(Vector3 position)
    {
        //position = V3Rounder(this.transform.position);
        //Instantiate(bombprefab);
        if (hasAuthority)
        {
            //placerPLayer = 0;
        }
        RpcPlaceABomb(position);
    }
    [ClientRpc]
    void RpcPlaceABomb(Vector3 position)
    {
        if (hasAuthority)
        {
            return;
        }
        //position = V3Rounder(this.transform.position);
        //placerPLayer = 1;
        //Instantiate(bombprefab);
        bombPlacerScript.PlaceABomb(position);
    }

    //Health
    public int MinusOneHealth()
    {
        health--;
        return health;
    }
    /*
    [Command]
    void CmdMinusOneHealth()
    {
        if (hasAuthority)
        {

        }
        RpcMinusOneHealth();
    }
    [ClientRpc]
    void RpcMinusOneHealth()
    {
        health--;
    }*/

    public void ShowLost()
    {
        if (hasAuthority)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void ShowWin()
    {
        if (hasAuthority){
            SceneManager.LoadScene(2);
        }
    }
    

    //Round the x,y,z of a Vector3
    Vector3 V3Rounder(Vector3 v3)
    {
        int x = (int)Math.Round(v3.x);
        int y = (int)Math.Round(v3.y);
        int z = (int)Math.Round(v3.z);
        return new Vector3(x, y, z);
    }

    //After 2 sec u can place a new bob
    IEnumerator BombPlaceCountDownCoroutine()
    {
        yield return new WaitForSeconds(2.3f); //Explotiontime + Bombdelettime + puffer(0.05)
        placedBombs--;
        
    }

    private Quaternion LookingInRightDirection(float x, float z)
    {
        if (x > 0)
        {
            if (z > 0)
            {
                //childPrefabtransform.transform.rotation = lookUpRight;
                return lookUpRight;
            }
            else if(z < 0)
            {
                //childPrefabtransform.transform.rotation = lookDownRight;
                return lookDownRight;
            }
            else
            {
                //childPrefabtransform.transform.rotation = lookRight;
                return lookRight;
            }
        }
        else if(x < 0)
        {
            if (z > 0)
            {
                //childPrefabtransform.transform.rotation = lookUpLeft;
                return lookUpLeft;
            }
            else if (z < 0)
            {
                //childPrefabtransform.transform.rotation = lookDownLeft;
                return lookDownLeft;
            }
            else
            {
                //childPrefabtransform.transform.rotation = lookLeft;
                return lookLeft;
            }
        }
        else
        {
            if (z > 0)
            {
                //childPrefabtransform.transform.rotation = lookUp;
                return lookUp;
            }
            else if (z < 0)
            {
                //childPrefabtransform.transform.rotation = lookDown;
                return lookDown;
            }
            else
            {
                //Change Nothing
                return lookingRotation;
            }
        }
    }
    
}
