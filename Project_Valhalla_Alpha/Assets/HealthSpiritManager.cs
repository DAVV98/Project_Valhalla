using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpiritManager : MonoBehaviour
{
    // this class is largely a copy of CameraController.cs
    // see that script for source code references

    public Transform playerTransform;
    private Vector3 offset;
    public float smoothSpeed = 2.0f;

    private void Awake()
    {
        offset = transform.position - playerTransform.position;
    }

    private void FixedUpdate()
    {
        MoveManager();
    }

    private void MoveManager()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
