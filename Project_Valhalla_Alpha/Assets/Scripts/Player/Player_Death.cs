using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : MonoBehaviour
{


    void OnCollisionEnter(Collision killCollision)
    {
        // kill player if collided with enemy
        if (killCollision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
