﻿using System.Collections;
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
        lr.SetPosition(1, endPos.position);
    }
}
