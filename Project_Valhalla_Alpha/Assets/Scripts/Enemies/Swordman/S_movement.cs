using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_movement : MonoBehaviour
{
    [SerializeField] trigger_ai trigger;
    public Transform target;
    public float MovementSpeed = 3f;
    public float weight;

    public float minDist;
    public bool canFall;
    void Start()
    {
        canFall = false;
    }

    void Update()
    {
        if (trigger.follow == true)
        {

            transform.LookAt(target);

            transform.position += transform.forward * MovementSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < minDist)
            {
                transform.position -= transform.forward * MovementSpeed * Time.fixedDeltaTime;
            }

        }
        else
        {

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

        

        // Vector3 pos = target.position - transform.position;
        // Quaternion rotation = Quaternion.LookRotation(pos);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);

    }

    void Move()
    {
       
    }

    void shieldPush()
    {
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(-(forceDirection * weight));
    }
}
