using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HUDNetworkManager : NetworkManager
{
   
    public void StarHost()
    {
        Debug.Log("Mhm");
        SetPort();
        NetworkManager.singleton.StartHost();
    }

   

    public void StartClient()
    {
        SetPort();
        SetIPAdress();
        NetworkManager.singleton.StartClient();
    }

    private void SetIPAdress()
    {
        string ipAdress = GameObject.Find("InputFieldIP").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAdress;
    }

    private void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    private void OnLevelWasLoaded(int level)
    {

        
        if (level == 0)
        {
            SetupMenuSceneButtens();
        }
        else if(level ==1)
        {
            SetupMainSceneButtens();
        }
    }

    void SetupMenuSceneButtens()
    {
        
        GameObject.Find("ButtonHostServer").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonHostServer").GetComponent<Button>().onClick.AddListener(StarHost);

        GameObject.Find("ButtonJoin").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoin").GetComponent<Button>().onClick.AddListener(StartClient);
        
    }

    void SetupMainSceneButtens()
    {
        GameObject.Find("ButtonDisconect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconect").GetComponent<Button>().onClick.AddListener(StopAll);
    }

    public void StopAll()
    {
        NetworkManager.singleton.StopHost();
    }

}
