using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnitOld : NetworkBehaviour
{

    //public GameObject playerModel;

    //Vector3 starPosionoenPlayer1 = new Vector3(0, 1, 0);
    /*
    public GameObject playerUnitPrefab;

    
    GameObject playerUnit;


    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private Animator m_animator;


    private float m_currentV = 0;
    private float m_currentH = 0;
    private readonly float m_interpolation = 10;
    */
    // Start is called before the first frame update
    void Start()
    {
        /*
        playerUnit = transform.gameObject;
        playerModel = playerUnit.transform.Find("MaleFreeSimpleMovement1").gameObject;
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!hasAuthority)
        {
            return;
        }*/
        //------------------------INPUTS------------------------//
        //Space = Up
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Translate(0, 1, 0);
        }*/
        //D = delete
        /*if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }*/
        //Movement
        /*
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        if (v > 0)
        {
            Debug.Log("V = 1");
        }
        else if (v < 0)
        {
            Debug.Log("V = -1");
        }
        if (h > 0)
        {
            Debug.Log("H = 1");
        }
        else if (h < 0)
        {
            Debug.Log("H = -1");
        }*/

    }

}
