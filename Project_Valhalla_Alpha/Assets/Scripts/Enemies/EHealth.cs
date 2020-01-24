using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHealth : MonoBehaviour
{
    private int health;
    
    // Start is called before the first frame update
    void Start()
    {
        //enemy health

        health = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        //kill enemy
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }



    }

    // collide with death wall 
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == ("Death"))
        {
            health += -999;

        }
    }
}
