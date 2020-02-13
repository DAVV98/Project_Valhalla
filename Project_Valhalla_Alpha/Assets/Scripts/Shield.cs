using System.Collections;
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
    //public float pushRange = 10.0f;
    public float pushForce = 400.0f;
    //public float pushRadius = 0.0f;

    public Player_v3 player;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player_v3>();
    }

    private void FixedUpdate()
    {
        //if (!bDidHit) {
            MoveShield();
        //}
        AgeShield();
    }

    IEnumerator WaitTime(float t = 3.0f)
    {
        yield return new WaitForSeconds(t);
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
        else if (other.CompareTag("Enemy") || other.CompareTag("Pushable"))
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
}
