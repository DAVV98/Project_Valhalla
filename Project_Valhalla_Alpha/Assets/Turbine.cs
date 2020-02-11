using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{


    public BoxCollider WindRange;
    public GameObject windArea;
    public GameObject BlockObject;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {

        //if (other.gameObject.tag == "WindPush")
        //{
        //    Debug.Log("rgrfrfr");
        //    windArea = other.gameObject;
        //    windArea.GetComponent<Rigidbody>().AddForce(transform.forward * 100);


        //}
        //This one uses a tag

        if (other.gameObject.GetComponent<MoveByWind>())
        {
            Debug.Log("Enter");
            windArea = other.gameObject;
            windArea.GetComponent<Rigidbody>().AddForce(transform.forward * 20);


        }
        //This one uses a script check





        if (other.gameObject.GetComponent<WindBlock>())
        {
            BlockObject = other.gameObject;

            //Vector3 blockDist = new Vector3(
            //    BlockObject.transform.position.x - this.transform.position.x,
            //    BlockObject.transform.position.y - this.transform.position.y,
            //    BlockObject.transform.position.z - this.transform.position.z
            //    );
            //Vector3 blockDist = new Vector3(3, 3, 3);


            Vector3 blockDist = new Vector3(
                1,1,
                BlockObject.transform.position.z - this.transform.position.z-6
                );

            //this.gameObject.transform.localScale = blockDist;
            WindRange.size = blockDist;
            

            Debug.Log("Wind blocked");

        }






    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        windArea.GetComponent<Rigidbody>().AddForce(transform.forward *-20);
    }




    



}
