﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public bool bActive = false;
    public Arrow arrowPrefab;
    private int shootTimer = 0;
    private int shootInterval = 50;
    public bool bMultiDirections = true;

    public float arrowSpeed = 6.0f;


    private void FixedUpdate()
    {
        ShootArrows();
        ShrinkSize();
    }

    // improvements
    //  1. could use object pooling for better performance
    private void ShootArrows()
    {
        if (bActive)
        {
            shootTimer++;

            if (shootTimer % shootInterval == 0)
            {
                if (bMultiDirections)
                {
                    //  2. could use for loop?
                    Arrow newArrow1 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow1.direction = gameObject.transform.right;

                    Arrow newArrow2 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow2.direction = gameObject.transform.right * -1;

                    Arrow newArrow3 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow3.direction = gameObject.transform.forward;

                    Arrow newArrow4 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow4.direction = gameObject.transform.forward * -1;
                }
                else
                {
                    Arrow newArrow = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow.direction = gameObject.transform.forward;
                }
                shootTimer = 0;
            }
        }
    }

    private void ShrinkSize()
    {
        // shrink size
        if (bActive)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = Vector3.one * 0.75f;
        }
    }
}
