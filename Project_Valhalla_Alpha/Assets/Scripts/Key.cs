using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GMaster GMaster;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /*
    void OnTriggerEnter(Collider other)
    {
        //if (other.transform.name == "player")
        if (other.CompareTag("Player"))
        {
            GMaster.Keys = +1;
            Destroy(this.gameObject);
        }
    }
    */
}
