﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    int Health;

    public GMaster GMaster;


    // Start is called before the first frame update
    public void Start()
    {
        Health = GMaster.Health;


    }

    // Update is called once per frame
    void Update()
    {



    }

    //enemy dmg
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == ("Enemy"))
        {
            Health = -1;

        }

        //prog dmg
        if (other.transform.tag == ("Progectile"))
        {
            Health = -1;

        }
    }

}
        
    
