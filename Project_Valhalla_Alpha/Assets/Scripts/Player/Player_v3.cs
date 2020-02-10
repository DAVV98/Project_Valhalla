﻿using System.Collections;
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
    public float fallSpeed = 4.0f;

    [Header("Shield")]
    public Transform shieldSpawn;
    public Shield shieldPrefab;
    public float shieldSpeed = 10.0f;
    //public int shieldHealth = 3;

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
        //Debug.Log(gameObject.transform.position.y);

        // for debugging
        if (bResetPressed())
        {
            shieldTimer = 0;
        }

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
            PlayerReset();
        }
    }

    private void PlayerReset()
    {
        bPlayerFalling = false;
        rb.MovePosition(playerSpawn.position);
        playerHealth = 3;
        fallSpeed = 4.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield")) {
            shieldTimer = 0;
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Projectile")) {
            if (bArmed) {
                playerHealth -= 1;
                //shieldHealth -= 1;
            }
            else {
                playerHealth -= 3;
            }

            if (other.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
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
        // decrease transform's y-component
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
        fallSpeed *= 1.01f;

        if (gameObject.transform.position.y <= playerMinHeight)
        {
            PlayerReset();
        }
    }

    private void ShieldThrow()
    {
        // instantiate shield
        Shield newShield = Instantiate(shieldPrefab, shieldSpawn.position, shieldSpawn.rotation);
        newShield.direction = shieldSpawn.forward;

        // improvements
        //  - player.shieldTimer should ideally be reset by the player with the use of Coroutines to continually evaluate newShield.bDidHit
        // if shield hits then reset timer
        if (newShield.bDidHit)
        {
            //Debug.Log("Player::ShieldThrow(), bDidHit = TRUE");
            shieldTimer = 0;
        }
    }

    bool bSpacePressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    bool bResetPressed()
    {
        return Input.GetKey(KeyCode.R);
    }

    private void MoveAndRotatePlayer()
    {
        float input_hori = Input.GetAxisRaw("Horizontal");
        float input_vert = Input.GetAxisRaw("Vertical");

        // use movement to create a new vector, normalize and scale
        Vector3 newPosition = new Vector3(input_hori, 0.0f, input_vert);
        newPosition.Normalize();
        newPosition = newPosition * moveSpeed * Time.fixedDeltaTime;
        
        // Look at move direction, and stay looking when not pressing.
        if (input_hori == -1 || input_hori == 1 || input_vert == -1 || input_vert == 1)
        {
            Vector3 movement = new Vector3(input_hori, 0.0f, input_vert);
            rb.MoveRotation(Quaternion.LookRotation(movement));
        }

        // use vector to move player
        rb.MovePosition(transform.position + newPosition);
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