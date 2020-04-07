using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroShieldPickup : MonoBehaviour
{
    public GameObject invisibleWall;
    public AudioSource audioSource_shieldPickup;
    private bool bActivateSoundPlayed = false;

    // when player triggers this object:
    //  1. enable shield
    //  2. deactivate invisible wall
    //  3. deactivate this object
    // note that the Player.bArmed should be disabled in the scene, and the shieldTimer set very high
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bActivateSoundPlayed == false)
            {
                audioSource_shieldPickup.Play();
                bActivateSoundPlayed = true;
            }

            other.gameObject.GetComponent<Player_v5>().shieldTimer = 0;
            invisibleWall.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            //gameObject.SetActive(false);
        }
    }
}
