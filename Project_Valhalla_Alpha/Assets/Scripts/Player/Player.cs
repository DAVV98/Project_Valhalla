using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("State")]
    public bool bPlayerFalling = false;
    public float moveSpeed = 5.0f;
    private float playerMinHeight = -4.0f;
    private int lookDirection = 0;
    public Transform playerSpawn;

    [Header("Shield")]
    bool bShieldThrowing = false;
    private float shieldThrowTimer;
    public float shieldSpeed = 10.0f;
    bool bShieldPushing = false;
    public Transform shieldSpawn;
    public GameObject shieldPrefab;


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
    }

    private void FixedUpdate()
    {
        MoveAndRotatePlayer();

        if (bPlayerFalling)
        {
            PlayerFall();
        }

        bShieldThrowing = spacePressed();

        if (bShieldThrowing)
        {
            ShieldThrow();
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
        transform.Translate((Vector3.up * -1.0f) * Time.deltaTime * fallSpeed);

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

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Pitfall"))
    //    {
    //        bPlayerFalling = true;
    //    }
    //}

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

    bool spacePressed()
    {
        return Input.GetKey("space") && shieldThrowTimer <= 0;
    }

    private void MoveAndRotatePlayer()
    {
        //Get Axis
        float input_horizontal = Input.GetAxisRaw("Horizontal");
        float input_vertical = Input.GetAxisRaw("Vertical");

        //Create x,y,z movement.
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
            transform.rotation = Quaternion.LookRotation(movement);
        }

        //use vector to move player.
        transform.position = transform.position + newPosition;
    }

    //void MovePlayer()
    //{
    //    float input_horizontal = Input.GetAxisRaw("Horizontal");
    //    float input_vertical = Input.GetAxisRaw("Vertical");

    //    // force movement to four directions
    //    if (input_horizontal != 0)
    //    {
    //        input_vertical = 0;
    //    }
    //    if (input_vertical != 0)
    //    {
    //        input_horizontal = 0;
    //    }

    //    float x = input_horizontal * moveSpeed * Time.fixedDeltaTime;
    //    float y = 0.0f;
    //    float z = input_vertical * moveSpeed * Time.fixedDeltaTime;

    //    Vector3 newPosition = new Vector3(x, y, z);
    //    //transform.position = transform.position + newPosition;

    //    //rb.velocity = newPosition;

    //    rb.MovePosition(transform.position + newPosition);
    //}

    //void RotatePlayer()
    //{

    //}
}
