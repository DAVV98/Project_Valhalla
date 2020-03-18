using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Reflector : MonoBehaviour
{
    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Laser")
        {
            child.SetActive(true);
        }
    }
}
