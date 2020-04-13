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

    public bool activate;

   

   
    private void Start()
    {
        if(activate == true)
        {
            activateObject.SetActive(false);
        }
        else
        {
            activateObject.SetActive(true);
        }

        
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

                if(activate == true)
                {
                    activateObject.SetActive(true);
                   

                    Debug.Log(1);
                }
                else
                {
                    activateObject.SetActive(false);

              
                }
            }
            else
            {
                // play sound
                audioSource_deactivate.Play();

                if (activate == true)
                {
                    activateObject.SetActive(false);
                
                }
                else
                {
                    activateObject.SetActive(true);
              
                }
            }

            Destroy(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        //door.SetActive(!Active);

    

    }
}