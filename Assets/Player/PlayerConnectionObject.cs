using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    //------------------------------------------------Public Vars------------------------------------------------//
    public GameObject PlayerUnitPrefab;
    GameObject playerGO;
    //Vector3[] spawnPoints = new Vector3[] { new Vector3(-6, 0, 6), new Vector3(6, 0, -6), new Vector3(6, 0, 6), new Vector3(-6, 0, -6) };


    //------------------------------------------------SyncVars------------------------------------------------//
    //SyncVars are variables, if this var changed on the server, all clients are automatically informed of the new var.
    //hook: WARNING! If using hook on a SyncVar, the local value does NOT get automatically changed!!
    //  in a hook SyncVar are not working!? Prevent loops on the server...

    //[SyncVar]
    //int playerCounter = 0;

    //------------------------------------------------Private Vars------------------------------------------------//


    
    // Start is called before the first frame update
    void Start()
    {
        
        if (!isLocalPlayer)
        {
            return;
        }
        Debug.Log("befor Spawn Player");
        CmdSpawnPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //------------------------------------------------INPUTS------------------------------------------------//
        //S = spawn
        /*if (Input.GetKeyDown(KeyCode.G))
        {
            CmdSpawnPlayer();
        }*/
        //Q
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            string n = "Quill" + Random.Range(1, 100);
            CmdChangePlayername(n);
        }*/
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdMovePlayerUp();
        }*/
    }

    //------------------------------------------------hook Functions------------------------------------------------//
    /*void OnPLayerNameChanged(string n)
    {
        Debug.Log("OnPLayerNameChanged: Old: " + playerName + " New: " + n);
        playerName = n;
        gameObject.name = "PlayerConnectionObject [" + n + "]";
    }*/


    //------------------------------------------------Command------------------------------------------------//
    //Only executet on the server
    [Command]
    void CmdSpawnPlayer()
    {
        //An welcher Position starten?:
       

        playerGO = Instantiate(PlayerUnitPrefab, transform.position + new Vector3(0,(float)1,0), Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(playerGO, connectionToClient);
        
        //CmdCounterPlusPlus();
        
    }

    

    
    
}