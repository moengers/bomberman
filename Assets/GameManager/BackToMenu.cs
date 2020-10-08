using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{

    float time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoBackToMenu());
    }

    IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(time);
        //SceneManager.LoadScene(0);
        GameObject.Find("GameManager").GetComponent<HUDNetworkManager>().StopAll();
    }
}
