﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_movement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float MovmentSpeed = 10f;
    [SerializeField] float rotationalDamp = 0.5f;

    void Update()
    {
        turn();
        Move();
    }

    void turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);

    }

    void Move()
    {
       transform.position += transform.forward * MovmentSpeed * Time.deltaTime;
    }
}