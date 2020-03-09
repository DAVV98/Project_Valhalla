using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_movement : MonoBehaviour
{
    public Transform target;
    public float MovementSpeed = 3f;
    public float weight;

    public float Dist;
    public bool canFall;
    void Start()
    {
        canFall = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < Dist)
        {
            turn();
            Move();
        }
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Shield")
        {
            shieldPush();
            canFall = true;
        }

        if (collision.tag == "Invisable_Wall")
        {
            if (canFall == false)
            {
                
            }
            else
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.GetComponent<Collider>());
            }

        }
    }
    void turn()
    {

        transform.LookAt(target);

        // Vector3 pos = target.position - transform.position;
        // Quaternion rotation = Quaternion.LookRotation(pos);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);

    }

    void Move()
    {
       transform.position += transform.forward * MovementSpeed * Time.deltaTime;
    }

    void shieldPush()
    {
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(-(forceDirection * weight));
    }
}
