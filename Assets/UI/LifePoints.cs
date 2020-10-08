using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    public Text text1;
    public Text text2;
    GameObject[] allPlayerArray = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        text1.text = "Player1 (Host): 3";
        text1.text = "   Player2    : -";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifePoints();
    }

    void UpdateLifePoints()
    {
        UpdatePlayer();
        if(allPlayerArray.Length >=1)
        {
            text1.text = "Player1 (Host): " + allPlayerArray[0].GetComponent<PlayerUnit>().health;
        }
        if(allPlayerArray.Length >= 2)
        {
            text2.text = "   Player2    : " + allPlayerArray[1].GetComponent<PlayerUnit>().health;
        }
    }

    void UpdatePlayer()
    {
        allPlayerArray = GameObject.FindGameObjectsWithTag("PlayerUnit");
    }
}
