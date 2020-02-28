using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // GameObject so we can access player.bPlayerFalling
    private Vector3 offset;
    public float smoothSpeed = 10.0f;
    

    private void Awake() {
        // offset borrowed from: https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c#5c7f8528edbc2a002053b711
        offset = transform.position - player.transform.position;
    }

    // research on LateUpdate() or FixedUpdate():
    //  1. https://forum.unity.com/threads/solved-camera-jitter-as-soon-as-using-lerp.762116/
    //  2. https://starmanta.gitbooks.io/unitytipsredux/content/all-my-updates.html
    // LateUpdate(), or FixedUpdate(), if moving player via Rigidbody physics
    private void FixedUpdate() {
        MoveCamera();
    }

    private void LateUpdate()
    {
        // this solution is a little messy
        // the reason this is called in LateUpdate() is because each
        // Island resets bFade to false, in Update(), so we want to fade it
        // after this Update() call
        HideWalls();
    }

    private void MoveCamera()
    {

        if (!player.GetComponent<Player_v3>().bPlayerFalling)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            // Vector3.Lerp() from borrowed from: https://www.youtube.com/watch?v=MFQhpwc6cKE
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // make the camera fixed on the player
            //transform.LookAt(player.transform);
        }
    }

    private void HideWalls()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        //Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Island"))
            {
                hit.transform.gameObject.GetComponent<Island>().bFade = true;
            }
        }
    }
}
