using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger_old : MonoBehaviour
{
    public GameObject door;
    public bool bActive = false;
    public Material activeMaterial;

    public AudioSource audioSource_activate;
    public AudioSource audioSource_doorOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && bActive == false)
        {
            bActive = true;
            door.SetActive(false);
            Destroy(other.gameObject);

            audioSource_activate.Play();
            audioSource_doorOpen.Play();
        }
    }

    private void Update()
    {
        if (bActive)
        {
            gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
        }
    }
}