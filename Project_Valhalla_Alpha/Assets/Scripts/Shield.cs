using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool bDidHit = false;
    int lifetime = 300;
    int age = 0;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
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

    private void OnTriggerEnter(Collider other)
    {
        bDidHit = true;

        if (other.CompareTag("Projectile")) {
            //ReflectArrow();
        }
        else if (other.CompareTag("Enemy")) {
            //PushEnemy();
        }
        else if (other.CompareTag("Box")) {
            //PushBlock();
        }

        // destroy shield if triggered
        Destroy(gameObject);
    }
}
