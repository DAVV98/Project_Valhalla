using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{
    private GameObject windArea;

    public int windForce = 20;
    public bool bActive = false;

    public AudioSource audioSource_turbineBlow;
    public float fadeSpeed = 1.5f;
    public float maxVolume = 0.3f;
    public float minVolume = 0.0f;

    private void Start()
    {
        audioSource_turbineBlow.Play();
    }

    private void FixedUpdate()
    {
        if (bActive)
        {
            ShootRay();
        }

        PlaySound();
    }

    void PlaySound()
    {
        float currentVolume = audioSource_turbineBlow.volume;
        if (bActive == true)
        {
            audioSource_turbineBlow.volume = Mathf.Lerp(currentVolume, maxVolume, fadeSpeed * Time.deltaTime);
        }
        else
        {
            audioSource_turbineBlow.volume = Mathf.Lerp(currentVolume, minVolume, fadeSpeed * Time.deltaTime);
        }
    }
    
    // set active if player near
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bActive = true;
        }
    }

    // set inactive if player near
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bActive = false;
        }
    }

    private void ShootRay()
    {
        RaycastHit windRay;
        if (Physics.Raycast(transform.position, transform.forward, out windRay, 100.0f))
        {
            if(windRay.collider.tag == "Bee Enemy")
            {
                Destroy(windRay.collider.gameObject);
            }
            if (windRay.collider.attachedRigidbody != null)
            {
                if (windRay.collider.GetComponent<MoveByWind>())
                {
                    windArea = windRay.collider.gameObject;
                    windArea.GetComponent<Rigidbody>().AddForce(transform.forward * windForce);

                    if (windRay.collider.GetComponent<Player_v5>())
                    {
                        Player_v5 player = windRay.collider.GetComponent<Player_v5>();

                        // limit movement speed
                        player.moveSpeed = 2;

                        // set player falling
                        //player.bPlayerFalling = true;

                        // turn off collision with invisible wall
                        player.gameObject.layer = 10;
                    }
                }
            }
        }

        // raycast through pitfall triggers
        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    RaycastHit hit = hits[i];

        //    if (hit.transform.gameObject.layer == 8)
        //    {
        //        //Debug.Log("Turbine::FixedUpdate(), RaycastAll[] hit stone block");
        //        return;
        //    }

        //    if (hit.collider.GetComponent<MoveByWind>()) {
        //        //Debug.Log("Turbine::FixedUpdate(), RaycastAll[]");
        //        windArea = hit.collider.gameObject;
        //        windArea.GetComponent<Rigidbody>().AddForce(transform.forward * windForce);
        //        //return;
        //    }
        //}
    }
}
