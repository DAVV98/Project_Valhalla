using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [Header("Characteristics")]
    public float speed;
    public float maxDistance;

    [Header("Target")]
    private Transform player;
    private Vector3 Target;

    [Header("Death_Timer")]
    private float startDeathTimer;
    public float deathTimerMax;



    /// <summary>
    /// Start:W
    /// - Set player transform.
    /// - Set vector to target player.
    /// </summary>

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        startDeathTimer = 0;
    }

    /// <summary>
    /// FixedUpdate:
    /// - Travel towards enemy.
    /// </summary>

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.fixedDeltaTime);

        deathTimer();
    }
    
    /// <summary>
    /// deathTimer:
    /// - Timer to kill axe after x seconds.
    /// </summary>
   
    void deathTimer()
    {
        startDeathTimer += Time.fixedDeltaTime;

        if (startDeathTimer >= deathTimerMax)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// OnCollisionEnter:
    /// - Kills player and destroy this.gameObject on collision.
    /// </summary>
    /// <param name="killCollision"> creates collider instance </param>
  
    void OnCollisionEnter(Collision killCollision)
    {
       
        if (killCollision.gameObject.tag == "Player")
        {
            //Destroy(killCollision.gameObject);
            Destroy(this.gameObject);
        }
       
    }
}
