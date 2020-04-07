using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPushableBlock : MonoBehaviour
{
    public float seaLevel = -3.0f;
    public AudioSource audioSource_waterSplash;
    private bool bSplashSoundPlayed = false;

    private void Update()
    {
        if (transform.position.y <= seaLevel)
        {
            if (!bSplashSoundPlayed)
            {
                Debug.Log("ShieldPushableBlock::Update(), playing splash sound");
                audioSource_waterSplash.Play();
                bSplashSoundPlayed = true;
            }
        }

        // don't destroy immediately otherwise the sound won't play
        if (transform.position.y <= -60.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy arrows
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
