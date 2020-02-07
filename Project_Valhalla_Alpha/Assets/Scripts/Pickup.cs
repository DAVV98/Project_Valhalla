using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform TheDest;

    public Transform player;

    public float Dist;


    void Update()
    {


        if (Input.GetKey(KeyCode.Mouse0) && (Vector3.Distance(transform.position, player.position) < Dist))
        {
            this.transform.position = TheDest.position;
            this.transform.parent = GameObject.Find("Dest").transform;
        }

        else
        {
            this.transform.parent = null;
        }
        

    }

    



}
