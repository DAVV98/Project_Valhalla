using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    //public bool bActive = false;
    public GameObject activateObject;
    public bool Active;

    public AudioSource audioSource_activate;
    public AudioSource audioSource_deactivate;

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


            if (activateObject.activeSelf == true)
            {
                // play sound
                audioSource_activate.Play();

                activateObject.SetActive(false);
            }
            else
            {
                // play sound
                audioSource_deactivate.Play();

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