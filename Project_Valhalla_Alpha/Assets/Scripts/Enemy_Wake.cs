using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wake : MonoBehaviour
{
    public GameObject enemy;

    private void Start()
    {
        enemy.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Enemy_Wake::OnTriggerEnter");
            enemy.SetActive(true);
        }
    }
}
