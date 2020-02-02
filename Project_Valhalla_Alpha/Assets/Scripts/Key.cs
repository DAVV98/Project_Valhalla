using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GMaster GMaster;
    
    void OnTriggerEnter(Collider other)
    {
        //if (other.transform.name == "player")
        if (other.CompareTag("Player"))
        {
            GMaster.Keys = +1;
            Destroy(this.gameObject);
        }
    }
    
}
