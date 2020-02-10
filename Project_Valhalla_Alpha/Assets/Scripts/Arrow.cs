﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int lifetime = 50;
    public int age = 0;

    public float arrowSpeed = 6.0f;
    public Vector3 direction = Vector3.zero;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveArrow();
        AgeArrow();

        //transform.LookAt(transform.position + rb.velocity);
    }

    private void MoveArrow()
    {
        rb.velocity = direction * arrowSpeed;
    }

    private void AgeArrow()
    {
        // increment age
        if (age < lifetime) {
            age += 1;
        }

        if (age >= lifetime) {
            // destroy arrow if old
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Shield"))
    //    {
    //        ReflectArrowByRaycast(other);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Shield"))
    //    {
    //        ReflectArrowByCollision(collision);
    //    }
    //}

    //private void ReflectArrowByCollision(Collision collision)
    //{
    //    // code from: https://answers.unity.com/questions/352609/how-can-i-reflect-a-projectile.html
    //    Debug.Log("ReflectArrowByCollision() called");
    //    ContactPoint contact = collision.contacts[0];
    //    Vector3 oldVelocity = rb.velocity;
    //    Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);
    //    rb.velocity = reflectedVelocity;
    //    Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
    //    transform.rotation = rotation * transform.rotation;
    //    // end of code block
    //}

    //private void ReflectArrowByConstantRaycast()
    //{
    //    //int layerMask = 1 << 8;
    //    //layerMask = ~layerMask;

    //    Debug.Log("ReflectArrowByConstantRaycast() called");

    //    // reset age so arrow persists longer
    //    age = 0.0f;

    //    // user raycast to access vector normal for reflection
    //    RaycastHit hit;
    //    Ray ray = new Ray(transform.position, rb.velocity);

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        Vector3 direction = Vector3.Reflect(rb.velocity, hit.normal);
    //        rb.velocity = direction.normalized * arrowSpeed;

    //        Debug.Log("ReflectArrow() direction = " + direction);
    //    }
    //}

    //private void ReflectArrowByRaycast(Collider other)
    //{
    //    Debug.Log("ReflectArrowByRaycast() called");

    //    // stop shield in place
    //    other.attachedRigidbody.velocity = Vector3.zero;

    //    // stop arrow in place
    //    //rb.velocity = Vector3.zero;

    //    // reset age so arrow persists longer
    //    age = 0.0f;

    //    // user raycast to access vector normal for reflection
    //    RaycastHit hit;
    //    Ray ray = new Ray(transform.position, rb.velocity);

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        Vector3 direction = Vector3.Reflect(rb.velocity, hit.normal);
    //        rb.velocity = direction.normalized * arrowSpeed;

    //        Debug.Log("ReflectArrow() direction = " + direction);
    //    }

    //    // destroy shield
    //    //Destroy(other);
    //}
}
