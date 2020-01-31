using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Push : MonoBehaviour
{
    [Header("Shield_Push")]
    public float SP_Range;
    public float SP_Force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShieldPush();
        }
    }

    void ShieldPush()
    {
        //creates layermask to ignore player objects.
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        //sends ray forward.
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit shield_hit;
        if (Physics.Raycast(transform.position, fwd, out shield_hit, SP_Range, layerMask))
        {
            if (shield_hit.collider.tag == "Enemy" || shield_hit.collider.tag == "Pushable")
            {
                //Destroy(shield_hit.rigidbody.gameObject);

                shield_hit.rigidbody.AddForceAtPosition(SP_Force * fwd, shield_hit.point);
            }
        }

    }
}
