using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("State")]
    public bool bDidHit = false;
    public int lifetime = 120;
    public int age = 0;
    public int shieldSlowThreshold = 75;

    public float shieldSpeed = 5.0f;
    public Vector3 direction = Vector3.zero;
    public float fallSpeed = 0.25f;

    [Header("Push")]
    public float pushForce = 400.0f;

    private bool bFading = false;
    private Color oldColor;
    private Color fadeColor;

    [Header("Other")]
    public GameObject fadeParticlePrefab;
    public Player_v5 player;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player_v5>();

        // setup fade colors
        oldColor = gameObject.GetComponent<MeshRenderer>().material.color;
        fadeColor = gameObject.GetComponent<MeshRenderer>().material.color;
        fadeColor.a = 0.0f;
    }

    private void FixedUpdate()
    {
        MoveShield();
        AgeShield();

        if (bFading) {
            FadeShield();
        }
    }

    private void FadeShield()
    {
        if (age >= lifetime / 2)
        {
            //Instantiate(fadeParticlePrefab, transform);
        }
        
        // start flashing after shield is half it's lifetime
        if (age >= 3 * lifetime / 4 && age % 5 == 0) {
            Color flashColor = gameObject.GetComponent<MeshRenderer>().material.color;
            flashColor.a += 0.25f;
            //Color flashColor = oldColor;
            //flashColor.a = 0.5f;
            gameObject.GetComponent<MeshRenderer>().material.color = flashColor;
        } else {
            // map from shieldSlowThresold-lifetime, not 0-age, since when bFading 
            // is set to TRUE in MoveShield(), age must be at shieldSlowThreshold
            float time = map(age, shieldSlowThreshold, lifetime, 0, 1);
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(oldColor, fadeColor, time);
        }
    }

    // improvements
    //  - player.shieldTimer should ideally be reset by the player with the use of Coroutines to continually evaluate newShield.bDidHit
    private void OnTriggerEnter(Collider other)
    {
        bDidHit = true;
        
        player.shieldTimer = 0;
        //Debug.Log("Shield::OnTriggerEnter(), bDidHit = TRUE");

        if (other.CompareTag("Projectile")) {

            //Debug.Log("Shield::OnTriggerEnter(), other == Projectile");
            //ReflectArrowByRaycast(other);
            ReflectArrowByVelocity(other);
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Pushable") || other.CompareTag("Axe Enemy"))
        {
            Debug.Log("Shield::OnTriggerEnter(), other == Enemy or Pushable");
            PushOther(other);
        }

        // can't delete immediately otherwise there are issues with reflection
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        age = lifetime - 10;

        // could use a coroutine...
        //StartCoroutine(WaitTime(5.0f));
        //Destroy(gameObject);
    }

    private void ReflectArrowByVelocity(Collider other)
    {
        // reset age so arrow persists longer
        Arrow arrow = other.GetComponent<Arrow>();

        if (arrow != null)
        {
            // reset arrow age so it persists longer
            arrow.age = 0;
            arrow.direction = direction;
        }
    }

    private void ReflectArrowByRaycast(Collider other)
    {
        // reset age so arrow persists longer
        Arrow arrow = other.GetComponent<Arrow>();
        
        if (arrow != null)
        {
            // reset arrow age so it persists longer
            arrow.age = 0;

            Rigidbody arrowRigidbody = arrow.rb;

            // use raycast to access vector normal for reflection
            RaycastHit hit;
            Ray ray = new Ray(transform.position, rb.velocity);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = Vector3.Reflect(arrowRigidbody.velocity, hit.normal);
                arrow.direction = direction.normalized;

                Debug.Log("ReflectArrow() direction = " + direction);
            }
        }
    }

    private void PushOther(Collider other)
    {
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        Rigidbody otherRigidbody = other.attachedRigidbody;

        if (otherRigidbody != null) {
            otherRigidbody.AddForce(forceDirection * pushForce);
        }
    }

    private void MoveShield()
    {
        // decrease shieldSpeed over time
        if (age > shieldSlowThreshold)
        {
            shieldSpeed *= 0.95f;
            bFading = true;
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
        }
        // otherwise move along direction
        else {
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

    // function taken from post #4: https://forum.unity.com/threads/mapping-or-scaling-values-to-a-new-range.180090/
    public float map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }
}
