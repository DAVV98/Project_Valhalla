using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("State")]
    public bool bPlayerFalling = false;
    public float moveSpeed = 5.0f;
    public float playerMinHeight = -4.0f;
    private int lookDirection = 0;
    public Transform playerSpawn;

    [Header("Shield Throw")]
    public float shieldSpeed = 10.0f;
    public Transform shieldSpawn;
    public GameObject shieldPrefab;
    bool bShieldThrowing = false;
    private float shieldThrowTimer;

    [Header("Shield Push")]
    public float shieldPushRange = 10.0f;
    public float shieldPushForce = 1000.0f;
    public float shieldPushRadius = 0.0f;
    bool bShieldPushing = false;
    private float shieldPushTimer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // update shieldThrowTimer
        if (shieldThrowTimer > 0)
        {
            shieldThrowTimer -= 0.1f;
        }

        if (shieldPushTimer > 0)
        {
            shieldPushTimer -= 0.1f;
        }
    }

    private void FixedUpdate()
    {
        MoveAndRotatePlayer();

        bShieldThrowing = bCanThrow();
        bShieldPushing = bCanPush();

        if (bPlayerFalling)
        {
            PlayerFall();
        }

        if (bShieldThrowing)
        {
            ShieldThrow();
        }

        if (bShieldPushing)
        {
            ShieldPush();
        }
    }

    // falling could be improved:
    //  1. detect pitfall with a raycast pointing downwards
    //  2. the fallspeed could increase as player transform height decreases (to simulate acceleration)
    //  3. the player can currently float into the walls as they fall
    private void PlayerFall()
    {
        float fallSpeed = 4.0f;
        // decrease transform's y-component
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);

        if (gameObject.transform.position.y <= playerMinHeight)
        {
            gameObject.transform.position = playerSpawn.position;
            bPlayerFalling = false;
        }
    }

    private void ShieldThrow()
    {
        // reset timer
        shieldThrowTimer = 2.0f;

        // instantiate shield
        GameObject newShield = Instantiate(shieldPrefab, shieldSpawn.position, shieldSpawn.rotation);
        newShield.GetComponent<Rigidbody>().velocity = shieldSpawn.forward * shieldSpeed;

        // destroy shield after amount of time
        Destroy(newShield, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            // hp -1
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Pitfall"))
        {
            bPlayerFalling = true;
        }
    }

    bool bCanThrow()
    {
        return Input.GetKey(KeyCode.Space) && shieldThrowTimer <= 0;
    }

    bool bCanPush()
    {
        return Input.GetKey(KeyCode.LeftShift) && shieldPushTimer <= 0;
    }

    private void MoveAndRotatePlayer()
    {
        // get input
        float input_horizontal = Input.GetAxisRaw("Horizontal");
        float input_vertical = Input.GetAxisRaw("Vertical");

        // create x,y,z movement.
        float x = input_horizontal * moveSpeed * Time.fixedDeltaTime;
        float y = 0.0f;
        float z = input_vertical * moveSpeed * Time.fixedDeltaTime;

        // use movement to create a new vector.
        Vector3 newPosition = new Vector3(x, y, z);

        // Look at move direction, and stay looking when not pressing.
        if (input_horizontal == -1 || input_horizontal == 1 || input_vertical == -1 || input_vertical == 1)
        //if (Mathf.Abs(input_horizontal + input_vertical) > 0)
        {
            Vector3 movement = new Vector3(input_horizontal, 0.0f, input_vertical);
            //transform.rotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.LookRotation(movement));
        }

        // use vector to move player.
        //transform.position = transform.position + newPosition;
        rb.MovePosition(transform.position + newPosition);
    }

    private void ShieldPush()
    {
        // reset timer
        shieldPushTimer = 2.0f;

        //creates layermask to ignore player objects.
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        //sends ray forward.
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit shield_hit;

        if (Physics.SphereCast(transform.position, shieldPushRadius, fwd, out shield_hit, shieldPushRange, layerMask))
        {
            if (shield_hit.collider.tag == "Enemy" || shield_hit.collider.tag == "Pushable")
            {
                //Destroy(shield_hit.rigidbody.gameObject);

                shield_hit.rigidbody.AddForceAtPosition(shieldPushForce * fwd, shield_hit.point);

            }
        }

        //if (Physics.Raycast(transform.position, fwd, out shield_hit, shieldPushRange, layerMask))
        //{
        //    if (shield_hit.collider.tag == "Enemy" || shield_hit.collider.tag == "Pushable")
        //    {
        //        //Destroy(shield_hit.rigidbody.gameObject);

        //        shield_hit.rigidbody.AddForceAtPosition(shieldPushForce * fwd, shield_hit.point);

        //    }
        //}
    }
}
