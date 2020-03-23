using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform newPlayerSpawn;
    private PlayerSpawnManager playerSpawnManager;
    public int currentCharge = 3;

    public int chargeTimer = 0;
    public int chargeTimerRate = 50;
    public bool bCanHeal = false;
    public bool bShouldHeal = false;

    private void Awake()
    {
        playerSpawnManager = FindObjectOfType<PlayerSpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint::OnTriggerEnter : new currentCheckpoint = " + newPlayerSpawn.position);
            playerSpawnManager.currentCheckpoint = newPlayerSpawn;
            //PlayerPrefs.SetFloat("spawnX", newPlayerSpawn.position.x);
            //PlayerPrefs.SetFloat("spawnY", newPlayerSpawn.position.y);
            //PlayerPrefs.SetFloat("spawnZ", newPlayerSpawn.position.z);
            //Debug.Log("Checkpoint::OnTriggerEnter(), newPlayerSpawn = " + newPlayerSpawn.position.x + ", " + newPlayerSpawn.position.y + ", " + newPlayerSpawn.position.z);

            //float x = PlayerPrefs.GetFloat("spawnX");
            //float y = PlayerPrefs.GetFloat("spawnY");
            //float z = PlayerPrefs.GetFloat("spawnZ");
            //Debug.Log("Checkpoint::OnTriggerEnter(), PlayerPrefs spawn = " + x + ", " + y + ", " + z);

            //other.GetComponent<Player_v5>().playerSpawn = newPlayerSpawn;
        }
    }

    private void FixedUpdate()
    {
        if (chargeTimer > 0)
        {
            chargeTimer--;
        }
        else if (chargeTimer <= 0)
        {
            bCanHeal = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // if player has full health, then don't heal them
            bShouldHeal = (other.GetComponent<Player_v5>().playerHealth < 9);

            if (bShouldHeal && bCanHeal && currentCharge > 0)
            {
                other.GetComponent<Player_v5>().playerHealth += 3;
                currentCharge--;
                chargeTimer = chargeTimerRate;
                bCanHeal = false;
            }
        }
    }
}
