using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKill : MonoBehaviour
{

    private void OnParticleCollision(GameObject col)
    {
        if(col.tag == "Player")
        {
            Destroy(col.gameObject);
        }
    }
}
