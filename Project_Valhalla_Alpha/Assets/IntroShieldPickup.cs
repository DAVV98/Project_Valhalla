using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroShieldPickup : MonoBehaviour
{
    // when player triggers this object:
    //  1. enable shield
    //  2. destroy this object
    // note that the Player.bArmed should be disabled in the scene, and the shieldTimer set very high
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_v5>().shieldTimer = 0;
            Destroy(gameObject);
        }
    }
}
