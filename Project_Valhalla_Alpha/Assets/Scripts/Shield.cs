using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool bDidHit = false;
    public int lifetime = 300;
    public int shieldSlowThreshold = 75;
    public int age = 0;

    public float shieldSpeed = 5.0f;
    public Vector3 direction = Vector3.zero;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * shieldSpeed;

        if (age > shieldSlowThreshold) {
            shieldSpeed *= 0.95f;
        }

        if (shieldSpeed < 0.1f)
        {
            shieldSpeed = 0.0f;
        }

        // slow down in direction correlation to age
        //rb.velocity = direction * shieldSpeed * (1 - ((float) age / lifetime));

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
        else if (other.CompareTag("Pushable")) {
            //PushBlock();
        }

        // destroy shield if triggered
        Destroy(gameObject);
    }
}
