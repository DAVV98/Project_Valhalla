using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject player;
    public Transform currentCheckpoint;
    public static PlayerSpawnManager instance = null;

    // singleton model borrowed from: http://answers.unity.com/comments/1285644/view.html
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // delete copies of the singleton, not the original singleton
        if (GameObject.Find(gameObject.name) &&
            GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(GameObject.Find(gameObject.name));
        }
    }

    private void Start()
    {
        Debug.Log("PlayerSpawnManager::Start : currentCheckpoint = " + currentCheckpoint.position);
        //player.GetComponent<Player_v5>().SetPosition(currentCheckpoint.position);
        player.GetComponent<Player_v5>().rb.MovePosition(currentCheckpoint.position);
    }
}
