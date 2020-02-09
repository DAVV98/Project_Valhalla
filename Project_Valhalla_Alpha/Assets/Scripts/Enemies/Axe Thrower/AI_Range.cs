using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Range : MonoBehaviour
{
    //Target Player
    private GameObject Player;
    private Transform Player_Vec3;

    [Header("Characteristics")]
    public int moveSpeed;
    public float maxDist;
    public float minDist;

    [Header("Axe Throw")]
    public GameObject axeObject;
    public Transform axeSpawn;
    private float time_between_shots;
    public float start_time_between_shots;


   


    void Start()
    {
    
        //Find player.
        Player = GameObject.FindGameObjectWithTag("Player");

        //Get Player transform
        Player_Vec3 = Player.transform;

        //Set up timer.
        time_between_shots = start_time_between_shots;
    }

    
    void FixedUpdate()
    {
        Range_AI();
        Attack_Timer();
    }

    /// <summary>
    /// Range_AI:
    /// - Sets enemy to look at player.
    /// - makes player come towards or away depending on distance.
    /// </summary>
 
    void Range_AI()
    {
        transform.LookAt(Player_Vec3);

        //if further than minDist come towards
        if(Vector3.Distance(transform.position, Player_Vec3.position) > maxDist)
        {
            transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
        }
        //if too close move back.
        else if (Vector3.Distance(transform.position, Player_Vec3.position) < minDist)
        {
            transform.position -= transform.forward * moveSpeed * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Attack_Timer:
    /// - Creates a timer that goes off every x seconds.
    /// - every x seconds instantiate new Axe.
    /// </summary>

    void Attack_Timer() 
    {
        if (time_between_shots <= 0)
        {
            Instantiate(axeObject, axeSpawn.position, axeSpawn.rotation);
            time_between_shots = start_time_between_shots;
        }
        else
        {
            time_between_shots -= Time.fixedDeltaTime;
        }
    }


/*

    //Go towards player. 
     
    private IEnumerator move_towards()
    {
        //if distance > x
            //move towards
    }

    //Stop at x distance
    //Throw axe at quick speed
    private IEnumerator stop_throw()
    {
        //if distance == x
            //throw axe at high speed.
    }

    //go back to x distance
    private IEnumerator retreat()
    {
        //once stop_throw done
            //go towards x distance
    }

    //Repeat 


*/
}