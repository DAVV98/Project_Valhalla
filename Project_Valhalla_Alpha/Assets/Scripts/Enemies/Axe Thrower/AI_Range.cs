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
    float throwTimer;
    public float throwSpeed;
    public float throw_every;

    void Start()
    {
        throwTimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        throwTimer += Time.deltaTime;
        Range_AI();
        
    }

    /// <summary>
    /// - Makes enemy look at player.
    /// - Creates the movement AI for axeThrower. 
    /// - TODO: make axe throw ability.
    /// </summary>
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

        if (Vector3.Distance(transform.position, Player.position) < maxDist)
        {
            AttackTimer();
        }
    }

    void AttackTimer()
    {
        //Timer x time.
        if (throwTimer == throw_every)
        {
            Debug.Log("shoot");
        }
    }

    void Attack()
    {
        //instantiate new axe.
        GameObject newAxe = Instantiate(axeObject, axeSpawn.position, axeSpawn.rotation);

        //throw axe
        newAxe.GetComponent<Rigidbody>().velocity = axeSpawn.forward * throwSpeed;

    }
}

//call attack once every 5 seconds.