﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("State")]
    public bool bDidHit = false;
    public int lifetime = 300;
    public int age = 0;
    public int shieldSlowThreshold = 75;

    public float shieldSpeed = 5.0f;
    public Vector3 direction = Vector3.zero;
    public float fallSpeed = 0.25f;

    [Header("Push")]
    public float pushRange = 10.0f;
    public float pushForce = 400.0f;
    public float pushRadius = 0.0f;

    public Player_v3 player;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player_v3>();
    }

    private void FixedUpdate()
    {
        MoveShield();
        AgeShield();
    }

    // improvements
    //  - player.shieldTimer should ideally be reset by the player with the use of Coroutines to continually evaluate newShield.bDidHit
    private void OnTriggerEnter(Collider other)
    {
        bDidHit = true;
        
        player.shieldTimer = 0;
        Debug.Log("Shield::OnTriggerEnter(), bDidHit = TRUE");

        if (other.CompareTag("Projectile")) {

            Debug.Log("Shield::OnTriggerEnter(), other == Projectile");
            ReflectArrow(other);
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Pushable"))
        {
            Debug.Log("Shield::OnTriggerEnter(), other == Enemy or Pushable");
            PushOther(other);
        }

        // destroy shield if triggered
        Destroy(gameObject);
    }

    private void ReflectArrow(Collider other)
    {

    }

    private void PushOther(Collider other)
    {
        // sends ray forward
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);

        Rigidbody otherRigidbody = other.attachedRigidbody;

        if (otherRigidbody != null) {
            //otherRigidbody.AddForceAtPosition(forceDirection * pushForce, other.transform.position);
            otherRigidbody.AddForce(forceDirection * pushForce);
        }

        //// creates layermask to ignore player objects.
        //int layerMask = 1 << 8;
        //layerMask = ~layerMask;
        
        //RaycastHit shield_hit;

        //if (Physics.SphereCast(transform.position, pushRadius, fwd, out shield_hit, pushRange, layerMask))
        //{
        //    shield_hit.rigidbody.AddForceAtPosition(pushForce * fwd, shield_hit.point);
        //}
    }

    private void MoveShield()
    {
        // decrease shieldSpeed over time
        if (age > shieldSlowThreshold)
        {
            shieldSpeed *= 0.95f;
        }

        if (shieldSpeed < 0.1f)
        {
            shieldSpeed = 0.0f;
        }

        // if not over ground, enable gravity
        RaycastHit hit;
        float raycastRange = 3.0f;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, raycastRange)) {
            rb.useGravity = true;
        } else {
            rb.velocity = direction * shieldSpeed;
            //rb.AddForce(direction * shieldSpeed);
        }
    }

    private void AgeShield()
    {
        if (age < lifetime)
        {
            age += 1;
        }
        if (age >= lifetime)
        {
            // destroy shield if old
            Destroy(gameObject);
        }
    }
}
