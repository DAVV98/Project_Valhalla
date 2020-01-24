using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = GMaster.Health;

        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //enemy dmg
        void OnTriggerEnter(Collider other)
        {
            if (collided.transform.tag == ("Enemy"))
            {
                Health = -1;
                
            }
        }

        //prog dmg
        void OnTriggerEnter(Collider other)
        {
            if (collided.transform.tag == ("Progectile"))
            {
                Health = -1;

            }
        }
        */

    }
}
