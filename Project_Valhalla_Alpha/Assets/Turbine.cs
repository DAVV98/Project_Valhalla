using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{

    public GameObject windArea;
   
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        
            RaycastHit windRay;

        if(Physics.Raycast(transform.position, transform.forward, out windRay, 100))
        {

            if (windRay.collider.GetComponent<MoveByWind>())
            {
                Debug.Log("Enter");
                windArea = windRay.collider.gameObject;
                windArea.GetComponent<Rigidbody>().AddForce(transform.forward * 20);

            }

        }

    }








}
