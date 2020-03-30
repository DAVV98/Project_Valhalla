using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpiritManager : MonoBehaviour
{
    // this class is largely a copy of CameraController.cs
    // see that script for source code references

    public GameObject player;
    [HideInInspector] public Vector3 offset;
    [HideInInspector] public float smoothSpeed = 8.0f;
    public GameObject hSpirit1, hSpirit2, hSpirit3;
    private float waveAmplitude = 0.25f;

    private void Awake()
    {
        // this is now set in Player_v5::Awake()
        //offset = transform.position - player.transform.position;
    }

    private void Start()
    {
        // set smoothSpeed to high value initially so it can quickly reach player from its default Vector3.zero spawn
        smoothSpeed = 2000.0f;
    }

    private void FixedUpdate()
    {
        UpdateSpirits();
        MoveManager();
        smoothSpeed = 8.0f;
    }

    private void UpdateSpirits()
    {
        OscillateSpirits();
        int currentHealth = player.GetComponent<Player_v5>().playerHealth / 3;
        PositionManager(currentHealth);
        DisableSpirits(currentHealth);
    }

    private void DisableSpirits(int playerHealth)
    {
        if (playerHealth <= 2)
        {
            hSpirit1.SetActive(false);
        }
        else
        {
            hSpirit1.SetActive(true);
        }

        if (playerHealth <= 1)
        {
            hSpirit2.SetActive(false);
        }
        else
        {
            hSpirit2.SetActive(true);
        }

        if (playerHealth <= 0)
        {
            hSpirit3.SetActive(false);
        }
        else
        {
            hSpirit3.SetActive(true);
        }
    }

    private void PositionManager(int playerHealth)
    {
        // adjust offset so spirits are centered as they decrease
        if (playerHealth <= 2)
        {
            offset.x = -0.25f;
        }
        else if (playerHealth >= 2)
        {
            offset.x = 0;
        }

        if (playerHealth <= 1)
        {
            offset.x = -0.5f;
        }
        if (playerHealth <= 0)
        {
            offset.x = 0;
        }



        // move spirits closer in or further depending on player state: resetting, armed, not armed
        if (player.GetComponent<Player_v5>().bResetting)
        {
            waveAmplitude = Mathf.Lerp(waveAmplitude, 0.15f, smoothSpeed * Time.deltaTime);
        }
        else
        {
            if (player.GetComponent<Player_v5>().bArmed)
            {
                waveAmplitude = Mathf.Lerp(waveAmplitude, 0.25f, smoothSpeed * Time.deltaTime);
            }
            else
            {
                waveAmplitude = Mathf.Lerp(waveAmplitude, 0.5f, smoothSpeed * Time.deltaTime);
            }
        }
    }

    private void OscillateSpirits()
    {
        // move spirits up and down at different phase
        float theta = Time.time * 2;

        float y1 = 1.5f + waveAmplitude * Mathf.Sin(theta);
        Vector3 newPosition1 = hSpirit1.transform.position;
        newPosition1.y = y1;
        hSpirit1.transform.position = newPosition1;

        float y2 = 1.5f + waveAmplitude * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.167f));
        Vector3 newPosition2 = hSpirit2.transform.position;
        newPosition2.y = y2;
        hSpirit2.transform.position = newPosition2;

        float y3 = 1.5f + waveAmplitude * Mathf.Sin(theta + ((Mathf.PI * 2.0f) * 0.333f));
        Vector3 newPosition3 = hSpirit3.transform.position;
        newPosition3.y = y3;
        hSpirit3.transform.position = newPosition3;
    }

    private void MoveManager()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
