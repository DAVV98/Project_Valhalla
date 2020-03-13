﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int lifetime = 150;
    public int age = 0;

    public float arrowSpeed = 6.0f;
    public Vector3 direction = Vector3.zero;

    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveAndRotateArrow();
        AgeArrow();

    }

    private void MoveAndRotateArrow()
    {
        rb.velocity = direction * arrowSpeed;
        transform.LookAt(transform.position + rb.velocity);
    }

    private void AgeArrow()
    {
        if (age < lifetime) {
            age += 1;
        }

        if (age >= lifetime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Island"))
        {
            Destroy(gameObject);
        }
    }
}
