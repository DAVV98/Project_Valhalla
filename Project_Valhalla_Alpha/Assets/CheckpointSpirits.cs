using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpirits : MonoBehaviour
{
    // this class is largely a copy of HealthSpiritManager.cs

    public GameObject checkpoint;
    public Vector3 offset;
    public float smoothSpeed = 2.0f;
    public GameObject hSpirit1, hSpirit2, hSpirit3;
    public float waveAmplitude = 0.15f;

    private void Awake()
    {
        offset = transform.position - checkpoint.transform.position;
    }

    private void FixedUpdate()
    {
        MoveManager();
        UpdateSpirits();
    }

    private void UpdateSpirits()
    {
        OscillateSpirits();
        int currentCharge = checkpoint.GetComponent<Checkpoint>().currentCharge;
        PositionManager(currentCharge);
        DisableSpirits(currentCharge);
    }

    private void DisableSpirits(int currentCharge)
    {
        if (currentCharge <= 2)
        {
            hSpirit1.SetActive(false);
        }
        else
        {
            hSpirit1.SetActive(true);
        }

        if (currentCharge <= 1)
        {
            hSpirit2.SetActive(false);
        }
        else
        {
            hSpirit2.SetActive(true);
        }

        if (currentCharge <= 0)
        {
            hSpirit3.SetActive(false);
        }
        else
        {
            hSpirit3.SetActive(true);
        }
    }

    private void PositionManager(int currentCharge)
    {
        // adjust offset so spirits are centered as they decrease
        if (currentCharge <= 2)
        {
            offset.x = -0.25f;
        }
        else if (currentCharge >= 2)
        {
            offset.x = 0;
        }

        if (currentCharge <= 1)
        {
            offset.x = -0.5f;
        }
        if (currentCharge <= 0)
        {
            offset.x = 0;
        }
    }

    private void OscillateSpirits()
    {
        // move spirits up and down at different phase

        float rate = 2;
        if (checkpoint.GetComponent<Checkpoint>().bShouldHeal)
        {
            rate = 4;
        }

        float theta = Time.time * rate;
        

        float y1 = 1.0f + waveAmplitude * Mathf.Sin(theta);
        Vector3 newPosition1 = hSpirit1.transform.position;
        newPosition1.y = y1;
        hSpirit1.transform.position = newPosition1;

        float y2 = 1.0f + waveAmplitude * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.167f));
        Vector3 newPosition2 = hSpirit2.transform.position;
        newPosition2.y = y2;
        hSpirit2.transform.position = newPosition2;

        float y3 = 1.0f + waveAmplitude * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.333f));
        Vector3 newPosition3 = hSpirit3.transform.position;
        newPosition3.y = y3;
        hSpirit3.transform.position = newPosition3;
    }

    private void MoveManager()
    {
        Vector3 desiredPosition = checkpoint.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
