using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_movement : MonoBehaviour
{
    public Transform target;
    [SerializeField] float MovmentSpeed = 10f;
    

    public float Dist;
    

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < Dist)
        {
            turn();
            Move();
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
       transform.position += transform.forward * MovmentSpeed * Time.deltaTime;
    }
}
