using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public bool bActive = false;
    public Arrow arrowPrefab;
    private int shootTimer = 0;
    public int shootInterval = 50;

    public bool bMultiDirections = false;

    public bool bRotate = false;
    public float rotateSpeed = 2.0f;
    private int rotateTimer = 0;

    private void FixedUpdate()
    {
        ShootArrows();
        //ShrinkSize();

        if (bRotate)
        {
            RotateTrap();
        }
    }

    private void RotateTrap()
    {
        if (bActive)
        {
            float yAngle = rotateTimer * rotateSpeed;
            Quaternion newRotation = Quaternion.Euler(0, yAngle, 0);
            transform.rotation = newRotation;
            rotateTimer++;
        }
    }

    // improvements
    //  1. could use object pooling for better performance
    private void ShootArrows()
    {
        if (bActive)
        {
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

            shootTimer++;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bActive = false;
        }
    }
}
