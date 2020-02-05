using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform newPlayerSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached checkpoint");

            other.GetComponent<Player>().playerSpawn = this.newPlayerSpawn;
        }
    }
}
