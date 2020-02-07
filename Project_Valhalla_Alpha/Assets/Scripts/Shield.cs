using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool bDidHit = false;
    public int lifetime = 300;
    public int age = 0;
    public int shieldSlowThreshold = 75;

    public float shieldSpeed = 5.0f;
    public Vector3 direction = Vector3.zero;

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
        //Debug.Log("Shield::OnTriggerEnter(), bDidHit = TRUE");

        if (other.CompareTag("Projectile")) {
            ReflectArrow(other);
        }
        else if (other.CompareTag("Enemy")) {
            PushEnemy(other);
        }
        else if (other.CompareTag("Pushable")) {
            PushBlock(other);
        }

        // destroy shield if triggered
        Destroy(gameObject);
    }

    private void ReflectArrow(Collider other)
    {

    }

    private void PushEnemy(Collider other)
    {

    }

    private void PushBlock(Collider other)
    {

    }

    private void MoveShield()
    {
        rb.velocity = direction * shieldSpeed;

        // decrease shieldSpeed over time
        if (age > shieldSlowThreshold)
        {
            shieldSpeed *= 0.95f;
        }

        if (shieldSpeed < 0.1f)
        {
            shieldSpeed = 0.0f;
        }

        // slow down in direction correlation to age
        //rb.velocity = direction * shieldSpeed * (1 - ((float) age / lifetime));
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
