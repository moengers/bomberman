using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerOnIt();    
    }

    private void CheckPlayerOnIt()
    {
        GameObject[] allPlayerArray = GameObject.FindGameObjectsWithTag("PlayerUnit");

        foreach(GameObject player in allPlayerArray)
        {
            //Check both on same position
            if(this.transform.position.x == RoundVec3(player.transform.position).x && this.transform.position.z == RoundVec3(player.transform.position).z)
            {
                player.GetComponent<PlayerUnit>().health++;
                Destroy(gameObject);
            }
        }
    }

    Vector3 RoundVec3(Vector3 v3)
    {
        return new Vector3((float)Math.Round(v3.x), (float)Math.Round(v3.y), (float)Math.Round(v3.z));
    }
}
