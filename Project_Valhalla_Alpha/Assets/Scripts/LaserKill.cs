﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKill : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player_v5>().DamagePlayer(9);
        }
        else if (other.tag == "ProjectileTrigger")
        {
            other.GetComponent<ProjectileTrigger>().Active = true;
        }

        if( other.tag == "Axe Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}