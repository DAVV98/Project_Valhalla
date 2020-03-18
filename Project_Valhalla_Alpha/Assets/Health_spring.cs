using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_spring : MonoBehaviour
{

    public float Timer = 1;
    public float IntervalSet;

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
        if (other.CompareTag("Player") == true)
        {
            IntervalSet = Timer;
        }
        
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player_v5>().playerHealth < 7)
            {
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
