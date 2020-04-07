using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform newPlayerSpawn;
    public int currentCharge = 3;

    public int chargeTimer = 0;
    public int chargeTimerRate = 50;
    public bool bCanHeal = false;
    public bool bShouldHeal = false;

    public AudioSource audioSource_checkpointActive;
    private bool bActivateSoundPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.position = newPlayerSpawn.position;

            if (bActivateSoundPlayed == false)
            {
                audioSource_checkpointActive.Play();
                bActivateSoundPlayed = true;
            }
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
                other.GetComponent<Player_v5>().HealPlayer();
                //other.GetComponent<Player_v5>().playerHealth += 3;
                currentCharge--;
                chargeTimer = chargeTimerRate;
                bCanHeal = false;
            }
        }
    }
}
