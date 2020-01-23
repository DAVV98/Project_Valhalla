using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Range : MonoBehaviour
{
    [Header("Target")]
    public Transform Player;

    [Header("Characteristics")]
    public int moveSpeed;
    public float maxDist;
    public float minDist;

    [Header("Axe Throw")]
    public GameObject axeObject;
    public Transform axeSpawn;
    public float throwSpeed;
    private float time_between_shots;
    public float start_time_between_shots;


    /// <summary>
    /// - Makes enemy look at player.
    /// - Creates the movement AI for axeThrower. 
    /// - TODO: make axe throw ability.
    /// </summary>
  
    void Start()
    {
        time_between_shots = start_time_between_shots;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Range_AI();
        Attack_Timer();
    }

    void Range_AI()
    {
        transform.LookAt(Player);

        //if further than minDist come towards
        if(Vector3.Distance(transform.position, Player.position) > maxDist)
        {
            transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
        }
        //if too close move back.
        else if (Vector3.Distance(transform.position, Player.position) < minDist)
        {
            transform.position -= transform.forward * moveSpeed * Time.fixedDeltaTime;
        }
    }
    
    void Attack_Timer()
    {

        if (time_between_shots <= 0)
        {
            Instantiate(axeObject, axeSpawn.position, axeSpawn.rotation);
            time_between_shots = start_time_between_shots;
        }
        else
        {
            time_between_shots -= Time.deltaTime;
        }

    }
    
}

//call attack once every 5 seconds.