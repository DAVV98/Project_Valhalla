using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
