using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_script : MonoBehaviour
{
    [SerializeField] Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( enemy.position.y <= -5 )
        {
            Destroy(gameObject);
        }
    }
}
