using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{
    private GameObject windArea;

    public int windForce = 20;

    private void FixedUpdate()
    {
        RaycastHit windRay;
        if (Physics.Raycast(transform.position, transform.forward, out windRay, 100.0f)) {
            if (windRay.collider.GetComponent<MoveByWind>()) {
                windArea = windRay.collider.gameObject;
                windArea.GetComponent<Rigidbody>().AddForce(transform.forward * windForce);
            }
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.gameObject.layer == 8)
            {
                Debug.Log("Turbine::FixedUpdate(), RaycastAll[] hit stone block");
                return;
            }

            if (hit.collider.GetComponent<MoveByWind>()) {
                Debug.Log("Turbine::FixedUpdate(), RaycastAll[]");
                windArea = hit.collider.gameObject;
                windArea.GetComponent<Rigidbody>().AddForce(transform.forward * windForce);
                //return;
            }
        }
    }
}
