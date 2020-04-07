using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_v5 : MonoBehaviour
{
    [Header("Movement")]
    public bool bPlayerFalling = false;
    public float moveSpeed = 250.0f;
    public float playerMinHeight = -4.0f;
    private int lookDirection = 0;
    public float fallSpeed = 4.0f;

    [Header("Health")]
    public int playerHealth = 9;
    //public GameObject hSpirit1, hSpirit2, hSpirit3;
    public bool bFlashing = false;
    private int flashTimer = 0;
    public int flashTimerRate = 15;
    private Color oldColor;
    private Color flashColor;

    [Header("Shield")]
    public Transform shieldSpawn;
    public Shield shieldPrefab;
    public float shieldSpeed = 10.0f;
    //public int shieldHealth = 3;
    public bool bArmed = false;
    public GameObject ArmedDisplay;
    public int shieldTimer = 0;
    public int shieldTimerRate = 60;

    [Header("Other")]
    public bool bResetting = false;
    public bool bGoingToReset = false;
    private float resetTimer = 0;
    private float resetTimerRate = 100;
    public Camera camera;
    [HideInInspector]  public Rigidbody rb;

    [Header("Sound")]
    public AudioSource audioSource_shieldPickup;
    public AudioSource audioSource_healthPickup;
    public AudioSource audioSource_playerHit;
    public AudioSource audioSource_playerFootsteps;
    private bool bShieldPickupSoundPlayed = true;
    public int footstepTimer = 0;
    public int footstepTimerRate = 20;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // setup flash colors
        oldColor = gameObject.GetComponent<MeshRenderer>().material.color;
        flashColor = gameObject.GetComponent<MeshRenderer>().material.color;
        flashColor.a = 0.25f;

        // initialise position
        transform.position = CheckpointManager.position;
        Vector3 cameraOffset = new Vector3(0.0f, 9.5f, -10.5f);
        Vector3 healthSpiritsOffset = Vector3.zero;
        FindObjectOfType<CameraController>().offset = cameraOffset;
        FindObjectOfType<HealthSpiritManager>().offset = healthSpiritsOffset;
    }

    private void FixedUpdate()
    {
        if (bArmed && bSpacePressed())
        {
            ShieldThrow();
            bArmed = false;
            shieldTimer = shieldTimerRate;
        }

        if (!bPlayerFalling)
        {
            MoveAndRotatePlayer();
        }
        else
        {
            PlayerFall();
        }

        // show shield if armed
        ArmedDisplay.SetActive(bArmed);

        if (bFlashing)
        {
            FlashTimer();
        }

        bResetting = bResetPressed();
        if (bResetPressed() )
        {
            StartPlayerReset();
        }
        else if (!bGoingToReset)
        {
            resetTimer = 0;
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 8.0f, Time.deltaTime * 5.0f);
        }

        if (shieldTimer > 0)
        {
            shieldTimer--;
        }
        else if (shieldTimer <= 0)
        {
            if (!bShieldPickupSoundPlayed)
            {
                // play sound
                audioSource_shieldPickup.Play();
                bShieldPickupSoundPlayed = true;
            }

            bArmed = true;
        }

        if (playerHealth <= 0)
        {
            PlayerReset();
        }
    }

    private void StartPlayerReset()
    {
        Debug.Log("Player::StartPlayerReset()");

        // camera zooms in on player
        float t = map(resetTimer, 0, resetTimerRate - 10, 0, 1);
        camera.orthographicSize = Mathf.Lerp(8.0f, 2.0f, t);

        // reset player
        if (resetTimer >= resetTimerRate)
        {
            //resetTimer = 0;
            bGoingToReset = true;
            Invoke("PlayerReset", 1.0f);
        }

        resetTimer += 1;
    }

    private void PlayerReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPosition(Vector3 pos)
    {
        rb.MovePosition(pos);
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

    public void DamagePlayer(int damageAmount) {
        // play sound
        audioSource_playerHit.Play();

        // flash player material
        bFlashing = true;

        // decrease health
        playerHealth -= damageAmount;
    }

    public void HealPlayer(int healAmount = 3)
    {
        // play sound
        audioSource_healthPickup.Play();

        // increase health
        playerHealth += healAmount;
    }

    // flash player material when hit
    private void FlashTimer() {
        if (flashTimer % 5 == 0) {
            gameObject.GetComponent<MeshRenderer>().material.color = flashColor;
        } else {
            gameObject.GetComponent<MeshRenderer>().material.color = oldColor;
        }

        if (flashTimer >= flashTimerRate) {
            flashTimer = 0;
            bFlashing = false;
            gameObject.GetComponent<MeshRenderer>().material.color = oldColor;
        }
        
        flashTimer += 1;
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
        // set shield pickup sound to false
        bShieldPickupSoundPlayed = false;

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
            Debug.Log("Player::bSpacePressed(), Player pressed SPACE");
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
        
        rb.MovePosition(transform.position + newPosition);

        // play random footstep sound
        if (input_hori != 0 || input_vert != 0)
        {
            if (footstepTimer % footstepTimerRate == 0)
            {
                audioSource_playerFootsteps.Play();
            }
            // increment foostepTimer after check so that foostep plays on first frame of player movement
            footstepTimer++;
        } else
        {
            // reset footstepTimer so that footstep plays on first frame of player movement
            footstepTimer = 0;
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
