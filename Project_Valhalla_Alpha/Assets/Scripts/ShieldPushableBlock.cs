using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPushableBlock : MonoBehaviour
{
    public float minHeight = -4.0f;

    private void Update()
    {
        if (transform.position.y <= minHeight)
        {
            Destroy(this.gameObject);
        }
    }
}
