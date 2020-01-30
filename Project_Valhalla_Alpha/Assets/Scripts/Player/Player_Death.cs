using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : MonoBehaviour
{

    void OnCollisionEnter(Collision killCollision)
    {

        if (killCollision.gameObject.tag == "Enemy")
        {
            Destroy(killCollision.gameObject);
            Destroy(this.gameObject);
        }

    }
}
