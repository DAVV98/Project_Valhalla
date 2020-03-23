using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKill : MonoBehaviour
{

    //private void OnParticleCollision(GameObject col)
    //{
    //    if(col.tag == "Player")
    //    {
    //        Destroy(col.gameObject);
    //    }
    //}

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "ProjectileTrigger")
        {
            other.GetComponent<ProjectileTrigger>().Active = true;
        }
    }
}
