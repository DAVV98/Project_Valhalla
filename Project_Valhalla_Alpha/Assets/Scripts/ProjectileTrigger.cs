using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    //public bool bActive = false;
    public GameObject door;
    public bool Active;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("trigger");
            Active = true;
            //bActive = true;
            //door.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    //private void FixedUpdate()
    //{
    //    door.SetActive(!bActive);
    //}
}