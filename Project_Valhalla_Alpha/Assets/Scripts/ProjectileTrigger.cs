using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    //public bool bActive = false;
    public GameObject activateObject;
    public bool Active;

    public AudioSource audioSource_activate;

    private void Start()
    {
        activateObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("trigger");
            Active = !Active;
            // activateObject.SetActive(true);

            // play sound
            audioSource_activate.Play();

            if (activateObject.activeSelf == true)
            {
                activateObject.SetActive(false);
            }
            else
            {
                activateObject.SetActive(true);
            }

            Destroy(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        //door.SetActive(!Active);
    }
}