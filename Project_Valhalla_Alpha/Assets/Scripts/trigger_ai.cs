using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_ai : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] GameObject Player;

    public bool follow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            follow = true;
        }
        else
        {

        }
    }
   

}
