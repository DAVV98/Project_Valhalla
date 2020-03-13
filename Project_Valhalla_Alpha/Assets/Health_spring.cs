using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_spring : MonoBehaviour
{

    public float Timer = 1;
    public float IntervalSet;

    //private GameObject Player;
    [SerializeField] Player_v5 player;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (IntervalSet > 0)
        {
            IntervalSet -= Time.deltaTime;


        }
        if (IntervalSet <= 0)
        {
            IntervalSet = 0;
            //Player.playerHealth += 1;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (CompareTag("Player") == true)
        {
            IntervalSet = Timer;
        }


        //Debug.Log("trigger enter");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("player found");
            if (other.GetComponent<Player_v5>().playerHealth < 7)
            {
                //Debug.Log("player given health from spring");
                other.GetComponent<Player_v5>().playerHealth += 3;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag("Player") == true)
        {
            IntervalSet = 0;
        }
    }
}
