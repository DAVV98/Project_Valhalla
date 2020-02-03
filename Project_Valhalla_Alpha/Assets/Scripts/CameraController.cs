using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // GameObject so we can access player.bPlayerFalling
    private Vector3 offset;
    public float smoothSpeed = 10.0f;

    private void Awake()
    {
        // offset borrowed from: https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c#5c7f8528edbc2a002053b711
        offset = transform.position - player.transform.position;
    }

    // research on LateUpdate() or FixedUpdate():
    //  1. https://forum.unity.com/threads/solved-camera-jitter-as-soon-as-using-lerp.762116/
    //  2. https://starmanta.gitbooks.io/unitytipsredux/content/all-my-updates.html
    private void LateUpdate() // LateUpdate() or FixedUpdate() if moving player via Rigidbody physics
    {
        if (!player.GetComponent<Player>().bPlayerFalling)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            // Vector3.Lerp() from borrowed from: https://www.youtube.com/watch?v=MFQhpwc6cKE
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // make the camera fixed on the player
            //transform.LookAt(player.transform);
        }
    }
}
