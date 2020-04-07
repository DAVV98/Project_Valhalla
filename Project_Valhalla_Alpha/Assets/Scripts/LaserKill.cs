using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKill : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("LaserKill::OnParticleCollision, player killed");
            other.GetComponent<Player_v5>().DamagePlayer(9);
        }
        else if (other.CompareTag("ProjectileTrigger"))
        {
            other.GetComponent<ProjectileTrigger>().Active = true;
        }

        if (other.CompareTag("Axe Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
