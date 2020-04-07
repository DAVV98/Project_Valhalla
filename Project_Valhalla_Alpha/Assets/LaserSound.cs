using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
    private bool bActive = false;
    public AudioSource audioSource_laserWhirr;

    public float fadeSpeed = 1.5f;
    public float maxVolume = 0.3f;
    public float minVolume = 0.0f;

    private void Start()
    {
        audioSource_laserWhirr.Play();
    }

    private void FixedUpdate()
    {
        PlaySound();
    }

    void PlaySound()
    {
        float currentVolume = audioSource_laserWhirr.volume;
        if (bActive == true)
        {
            audioSource_laserWhirr.volume = Mathf.Lerp(currentVolume, maxVolume, fadeSpeed * Time.deltaTime);
        }
        else
        {
            audioSource_laserWhirr.volume = Mathf.Lerp(currentVolume, minVolume, fadeSpeed * Time.deltaTime);
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
}
