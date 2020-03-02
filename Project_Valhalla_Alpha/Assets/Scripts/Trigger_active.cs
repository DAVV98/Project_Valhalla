using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_active : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ProjectileTrigger trigger;
    [SerializeField] Material Active_mat;

    public GameObject Trigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( trigger.Active == true)
        {
            Trigger.GetComponent<MeshRenderer>().material = Active_mat;
        }
    }
}
