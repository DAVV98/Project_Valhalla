using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger_old : MonoBehaviour
{
    public GameObject door;
    public bool bActive = false;
    public Material activeMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            bActive = true;
            door.SetActive(false);
            Destroy(other.gameObject);
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