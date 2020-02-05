using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform newPlayerSpawn;
    public GameObject activeDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached checkpoint");

            activeDisplay.SetActive(true);
            other.GetComponent<Player>().playerSpawn = this.newPlayerSpawn;
        }
    }
}
