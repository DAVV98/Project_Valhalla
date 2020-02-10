﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    //public bool bActive = false;
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            //bActive = true;
            door.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    //private void FixedUpdate()
    //{
    //    door.SetActive(!bActive);
    //}
}