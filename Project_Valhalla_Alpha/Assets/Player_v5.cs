﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_v5 : MonoBehaviour
{
    [Header("Movement")]
    public bool bPlayerFalling = false;
    public float moveSpeed = 250.0f;
    public float playerMinHeight = -4.0f;
    private int lookDirection = 0;
    public Transform playerSpawn;
    public float fallSpeed = 4.0f;

    [Header("Health")]
    public int playerHealth = 9;
    public GameObject hSpirit1, hSpirit2, hSpirit3;
    public bool bFlashing = false;
    private int flashTimer = 0;
    public int flashTimerRefreshRate = 15;
    private Color oldColor;
    private Color flashColor;

    [Header("Shield")]
    public Transform shieldSpawn;
    public Shield shieldPrefab;
    public float shieldSpeed = 10.0f;
    //public int shieldHealth = 3;

    public bool bArmed = true;
    public GameObject ArmedDisplay;
    public int shieldTimer = 0;
    public int shieldTimerRefreshRate = 60;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // setup flash colors
        oldColor = gameObject.GetComponent<MeshRenderer>().material.color;
        flashColor = gameObject.GetComponent<MeshRenderer>().material.color;
        flashColor.a = 0.25f;
    }

    private void Update()
    {
        if (bArmed && bSpacePressed())
        {
            ShieldThrow();
            bArmed = false;
            shieldTimer = shieldTimerRefreshRate;
        }
    }
    private void FixedUpdate()
    {
        if (bResetPressed())
        {
            //shieldTimer = 0;
        }

        if (!bPlayerFalling)
        {
            MoveAndRotatePlayer();
        }
        else
        {
            PlayerFall();
        }

        ArmedDisplay.SetActive(bArmed);

        DisplayHealth();

        if (bFlashing)
        {
            FlashTimer();
        }

        if (shieldTimer > 0)
        {
            shieldTimer--;
        }
        else if (shieldTimer <= 0)
        {
            bArmed = true;
        }

        if (playerHealth <= 0)
        {
            // game over screen
            PlayerReset();
        }
    }

    private void DisplayHealth()
    {
        // move spirits up and down at different phase
        float theta = Time.time * 2;
        float amp = 0.25f;

        float y1 = 1.5f + amp * Mathf.Sin(theta);
        Vector3 newPosition1 = hSpirit1.transform.position;
        newPosition1.y = y1;
        hSpirit1.transform.position = newPosition1;

        float y2 = 1.5f + amp * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.333f));
        Vector3 newPosition2 = hSpirit2.transform.position;
        newPosition2.y = y2;
        hSpirit2.transform.position = newPosition2;

        float y3 = 1.5f + amp * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.666f));
        Vector3 newPosition3 = hSpirit3.transform.position;
        newPosition3.y = y3;
        hSpirit3.transform.position = newPosition3;

        // move spirits closer in when resetting
        Vector3 offset1 = hSpirit1.transform.position - transform.position;
        float distance1 = offset1.magnitude;

        if (bResetPressed())
        {

        }

        // center spirits when removing them

        // disable spirits (could be improved with an array)
        int currentHealth = playerHealth / 3;
        if (currentHealth <= 2) {
            hSpirit1.SetActive(false);
        }
        if (currentHealth <= 1) {
            hSpirit2.SetActive(false);
        }
        if (currentHealth <= 0) {
            hSpirit3.SetActive(false);
        }
    }

    private void PlayerReset()
    {
        bPlayerFalling = false;
        transform.position = playerSpawn.position;
        rb.MovePosition(transform.position);
        playerHealth = 9;
        fallSpeed = 4.0f;
        rb.velocity *= 0;
        rb.angularVelocity *= 0;

        hSpirit1.SetActive(true);
        hSpirit2.SetActive(true);
        hSpirit3.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            shieldTimer = 0;
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Projectile"))
        {
            if (bArmed) {
                DamagePlayer(3);
                //shieldHealth -= 1;
            } else {
                DamagePlayer(3);
            }

            if (other.CompareTag("Projectile")) {
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Pitfall"))
        {
            bPlayerFalling = true;
        }
    }

    private void DamagePlayer(int damageAmount) {
        // flash player material
        bFlashing = true;

        // decrease health
        playerHealth -= damageAmount;
    }

    // flash player material when hit
    private void FlashTimer() {
        if (flashTimer % 5 == 0) {
            gameObject.GetComponent<MeshRenderer>().material.color = flashColor;
            Debug.Log("flash color");
        } else {
            gameObject.GetComponent<MeshRenderer>().material.color = oldColor;
            Debug.Log("normal color");
        }

        if (flashTimer >= flashTimerRefreshRate) {
            flashTimer = 0;
            bFlashing = false;
            gameObject.GetComponent<MeshRenderer>().material.color = oldColor;
        }
        
        flashTimer += 1;

        //Debug.Log(flashTimerCounter);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player pressed SPACE");
        }
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

        //transform.position = transform.position + newPosition;

        // use vector to move player
        //rb.MovePosition(transform.position + newPosition);
        //rb.AddForce(newPosition);
        rb.velocity = newPosition;
    }
}
