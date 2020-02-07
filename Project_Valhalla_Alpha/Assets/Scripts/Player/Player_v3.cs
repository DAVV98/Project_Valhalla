using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_v3 : MonoBehaviour
{

    [Header("State")]
    public bool bPlayerFalling = false;
    public float moveSpeed = 5.0f;
    public float playerMinHeight = -4.0f;
    private int lookDirection = 0;
    public Transform playerSpawn;
    public int playerHealth = 9;

    [Header("Shield")]
    public Transform shieldSpawn;
    public GameObject shieldPrefab;
    public float shieldSpeed = 10.0f;

    public bool bArmed = true;
    public GameObject ArmedDisplay;
    public int shieldTimer = 0;
    public int shieldTimerRefreshRate = 60;

    //bool bShieldThrowing = false;
    //private float shieldThrowTimer;

    //[Header("Shield Push")]
    //public float shieldPushRange = 10.0f;
    //public float shieldPushForce = 1000.0f;
    //public float shieldPushRadius = 0.0f;
    //bool bShieldPushing = false;
    //private float shieldPushTimer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveAndRotatePlayer();
        
        if (bPlayerFalling) {
            PlayerFall();
        }

        ArmedDisplay.SetActive(bArmed);

        if (bArmed && bSpacePressed()) {
            ShieldThrow();
            bArmed = false;
            shieldTimer = shieldTimerRefreshRate;
        }

        if (shieldTimer > 0) {
            shieldTimer--;
        } else if (shieldTimer <= 0) {
            bArmed = true;
        }

        if (playerHealth <= 0)
        {
            // game over screen
            gameObject.transform.position = playerSpawn.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield")) {
            shieldTimer = 0;
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Projectile")) {
            if (bArmed) {
                playerHealth -= 1;
            }
            else {
                playerHealth -= 3;
            }
        }

        if (other.CompareTag("Pitfall")) {
            bPlayerFalling = true;
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
        // instantiate shield
        GameObject newShield = Instantiate(shieldPrefab, shieldSpawn.position, shieldSpawn.rotation);
        newShield.GetComponent<Rigidbody>().velocity = shieldSpawn.forward * shieldSpeed;

        // if shield hits then reset timer
        if (newShield.GetComponent<Shield>().bDidHit)
        {
            shieldTimer = 0;
        }
    }

    bool bSpacePressed()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private void MoveAndRotatePlayer()
    {
        // get input
        float input_horizontal = Input.GetAxisRaw("Horizontal");
        float input_vertical = Input.GetAxisRaw("Vertical");

        // use movement to create a new vector
        Vector3 newPosition = new Vector3(input_horizontal, 0.0f, input_vertical);
        // normalize
        newPosition.Normalize();
        // scale
        newPosition = newPosition * moveSpeed * Time.fixedDeltaTime;
        
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

        // fix player spinning
        //if (input_horizontal + input_horizontal + 2 > 2)
        //{
        //    rb.velocity = Vector3.zero;
        //}
    }

    //private void ShieldPush()
    //{
    //    // reset timer
    //    shieldPushTimer = 2.0f;

    //    //creates layermask to ignore player objects.
    //    int layerMask = 1 << 8;
    //    layerMask = ~layerMask;

    //    //sends ray forward.
    //    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    //    RaycastHit shield_hit;

    //    if (Physics.SphereCast(transform.position, shieldPushRadius, fwd, out shield_hit, shieldPushRange, layerMask))
    //    {
    //        if (shield_hit.collider.tag == "Enemy" || shield_hit.collider.tag == "Pushable")
    //        {
    //            //Destroy(shield_hit.rigidbody.gameObject);

    //            shield_hit.rigidbody.AddForceAtPosition(shieldPushForce * fwd, shield_hit.point);

    //        }
    //    }
    //}
}
