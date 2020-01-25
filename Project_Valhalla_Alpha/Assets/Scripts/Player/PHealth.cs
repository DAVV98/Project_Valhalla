using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHealth : MonoBehaviour
{

    int Health;

    public GMaster GMaster;


    // Start is called before the first frame update
    void Start()
    {
        Health = GMaster.Health;

        Health += 4;

    }
    
    // Update is called once per frame
    void Update()
    {



    }

    //enemy dmg
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health = -1;

        }

        //prog dmg
        if (other.CompareTag("Progectile"))
        {
            Health = -1;

        }
    }

}
