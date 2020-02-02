using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public bool bActive = false;
    public GameObject arrowPrefab;
    private int shootTimer = 0;
    private int shootInterval = 50;
    public bool bMultiDirections = true;

    public float arrowSpeed = 6.0f;

    private void Update()
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

    // improvements
    //  1. could use object pooling for better performance
    private void FixedUpdate()
    {
        if (bActive)
        {
            shootTimer++;

            if (shootTimer % shootInterval == 0)
            {
                if (bMultiDirections)
                {
                    //  2. could use for loop?
                    GameObject newArrow1 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow1.GetComponent<Rigidbody>().velocity = gameObject.transform.right * arrowSpeed;

                    GameObject newArrow2 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow2.GetComponent<Rigidbody>().velocity = gameObject.transform.right * arrowSpeed * -1.0f;

                    GameObject newArrow3 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow3.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * arrowSpeed;

                    GameObject newArrow4 = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    newArrow4.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * arrowSpeed * -1.0f;
                }
                else
                {
                    GameObject newArrow = Instantiate(arrowPrefab, gameObject.transform.position, gameObject.transform.rotation);
                    newArrow.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * arrowSpeed;
                }
                shootTimer = 0;
            }
        }
    }
}
