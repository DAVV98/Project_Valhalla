using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Line : MonoBehaviour
{
    private LineRenderer lr;

    [Header("Position")]
    public Transform startPos;
    public Transform endPos;
   
    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = .2f ;
        lr.endWidth = .2f;
    }

    // Update is called once per frame
    void Update()
    {
        //set laser start and end position.
        lr.SetPosition(0, startPos.position);


        RaycastHit laser_hit;

        if(Physics.Raycast(startPos.position, -transform.forward, out laser_hit))
        {
            if(laser_hit.collider)
            {
                lr.SetPosition(1, laser_hit.point);
            }       
        }

        /*
        else
        {
            lr.SetPosition(1, endPos.position);
        }
       */
    }
}
