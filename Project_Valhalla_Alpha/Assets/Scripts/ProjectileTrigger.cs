using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    //public bool bActive = false;
    public GameObject activateObject;
    public bool Active;

    public GameObject bothActivate;

    public AudioSource audioSource_activate;
    public AudioSource audioSource_deactivate;

    public bool activate;

    public bool doBoth;
   

   
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
                    //Deactivates Object for Activate 
                    activateObject.SetActive(false);

                    if(doBoth == true)
                    {
                        bothActivate.SetActive(true);
                    }
                }
                else
                {
                    activateObject.SetActive(false);

                }
            }
            else
            {
                // play soundq
                audioSource_deactivate.Play();

                if (activate == true)
                {
                    //Activate object for activate.
                    activateObject.SetActive(true);


                    if (doBoth == true)
                    {
                        bothActivate.SetActive(false);
                    }
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