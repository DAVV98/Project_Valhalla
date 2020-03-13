using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public GameObject invisibleWall;

    public ArrowTrap[] arrowTraps;

    private void Awake()
    {
        arrowTraps = GameObject.FindObjectsOfType<ArrowTrap>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            invisibleWall.SetActive(true);
            
            for (int i = 0; i < arrowTraps.Length; ++i)
            {
                //arrowTraps[i].bRotate = true;
                arrowTraps[i].bActive = true;
            }
        }
    }
}
