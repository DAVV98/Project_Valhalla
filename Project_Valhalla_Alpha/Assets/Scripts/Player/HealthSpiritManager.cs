using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpiritManager : MonoBehaviour
{
    // this class is largely a copy of CameraController.cs
    // see that script for source code references

    public GameObject player;
    public Vector3 offset;
    public float smoothSpeed = 8.0f;
    public GameObject hSpirit1, hSpirit2, hSpirit3;

    private void Awake()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        UpdateSpirits();
        MoveManager();
    }

    private void UpdateSpirits()
    {
        // move spirits up and down at different phase
        float theta = Time.time * 2;
        float amp = 0.25f;

        float y1 = 1.5f + amp * Mathf.Sin(theta);
        Vector3 newPosition1 = hSpirit1.transform.position;
        newPosition1.y = y1;
        hSpirit1.transform.position = newPosition1;

        float y2 = 1.5f + amp * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.167f));
        Vector3 newPosition2 = hSpirit2.transform.position;
        newPosition2.y = y2;
        hSpirit2.transform.position = newPosition2;

        float y3 = 1.5f + amp * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.333f));
        Vector3 newPosition3 = hSpirit3.transform.position;
        newPosition3.y = y3;
        hSpirit3.transform.position = newPosition3;

        // move spirits closer in when resetting
        Vector3 offset1 = hSpirit1.transform.position - transform.position;
        float distance1 = offset1.magnitude;

        // disable spirits (could be improved with an array)
        // and adjust offset so spirits are centered
        int currentHealth = player.GetComponent<Player_v5>().playerHealth / 3;
        if (currentHealth <= 2)
        {
            offset = new Vector3(-0.25f, 0, 0);
            hSpirit1.SetActive(false);
        }
        if (currentHealth <= 1)
        {
            offset = new Vector3(-0.5f, 0, 0);
            hSpirit2.SetActive(false);
        }
        if (currentHealth <= 0)
        {
            offset = Vector3.zero;
            hSpirit3.SetActive(false);
        }
    }

    private void MoveManager()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
