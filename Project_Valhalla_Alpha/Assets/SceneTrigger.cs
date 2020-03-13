using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public GameObject invisibleWall;

    public ArrowTrap arrowTrap1;
    public ArrowTrap arrowTrap2;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            invisibleWall.SetActive(true);

            arrowTrap1.bActive = true;
            arrowTrap1.bRotate = true;
            arrowTrap2.bActive = true;
            arrowTrap2.bRotate = true;
        }
    }
}
