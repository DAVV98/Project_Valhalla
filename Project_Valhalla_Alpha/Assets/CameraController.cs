using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // camera tracking with offset borrow from: https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c#5c7f8528edbc2a002053b711
    public GameObject player;
    private Vector3 offset;

    void Start()
    {

        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (!player.GetComponent<Player>().bPlayerFalling)
            transform.position = player.transform.position + offset;
    }
}
