using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_AI : MonoBehaviour
{

    [Header("Characteristics")]
    public float moveSpeed;
    public float escapeSpeed;
    public float stopPos;
    public float retreatDistance;
    public enum Axe_State { Approach, Throw, Retreat, Dont_Fall, Fall, Dont_Move };
    public Axe_State currentState;
    public float weight;
    private GameObject Player;
    private Transform Player_Pos;


    [Header("Axe Throw")]
    public GameObject axeObject;
    public Transform axeSpawn;
    public float throwSpeed;
    public float max_Axes;
    private int thrown_axes;
    private float time_between_shots;
    public float start_time_between_shots;


    private bool canFall;

    void Start()
    {
        //Makes Player useable and gets its transfroms.
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Pos = Player.transform;

        //Start with approach state.
        currentState = Axe_State.Approach;

        //Set up timer.
        time_between_shots = start_time_between_shots - 0.8f;

        // set axes thrown to 0
        thrown_axes = 0;

        //sets so player can't fall at start
        canFall = false;
    }

    void Update()
    {
        //Switches between states and calles respective functions.
        switch (currentState)
        {
            case Axe_State.Approach:
                Approach();
                break;
            case Axe_State.Throw:
                Throw();
                break;
            case Axe_State.Retreat:
                Retreat();
                break;
            case Axe_State.Dont_Fall:
                Dont_Fall();
                break;

        }

    }

    void Approach()
    {
        //set so player cannot fall approaching
        canFall = false; //change

        //look at player
        transform.LookAt(Player_Pos);
        
        //move towards player
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;

        //if at stop pos changes state.
        if (Vector3.Distance(Player_Pos.position, this.transform.position) <= stopPos)
        {
            thrown_axes = 0;
            currentState = Axe_State.Throw;
        }
    }

    void Throw()
    {
        //look at players
        transform.LookAt(Player_Pos);
       
        //start timer.
        Attack_Timer();
        
        //thrown axe goes towareds player.
        axeObject.transform.LookAt(Player_Pos);

        //Change state to retreat.
        if (thrown_axes == max_Axes)
        {
            currentState = Axe_State.Retreat;
        }
    }

    void Retreat()
    {
        //set canfall to false
        canFall = false;

        //look at player
        transform.LookAt(Player_Pos);
        
        //move away from player
        transform.position -= transform.forward * (moveSpeed + escapeSpeed) * Time.fixedDeltaTime;

        //Stop at escape distance and start approach again.
        if (Vector3.Distance(Player_Pos.position, this.transform.position) >= retreatDistance)
        {
            currentState = Axe_State.Approach;
        }
    }

    void Dont_Fall()
    {
        currentState = Axe_State.Approach;
    }

    void Attack_Timer()
    {
        if (time_between_shots <= 0)
        {
            //instantiate new axe
            Instantiate(axeObject, axeSpawn.position, axeSpawn.rotation);
            //Increase axe counter
            thrown_axes++;
            time_between_shots = start_time_between_shots;
        }
        else
        {

            time_between_shots -= Time.fixedDeltaTime;
        }
    }

    void shieldPush()
    {
        //set direction of push
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        Rigidbody rb = this.GetComponent<Rigidbody>();

        //add force
        rb.AddForce(-(forceDirection * weight));
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Shield")
        {
            shieldPush();
            canFall = true;

        }

        if (collision.tag == "Enemy_Invisable_Wall")
        {
            if (canFall == false)
            {
                currentState = Axe_State.Approach;
            }
            else if (canFall == true)
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.GetComponent<Collider>());
            }

        }

        if (collision.tag == "laser")
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Shield")
        {
            canFall = false;

        }
    }
    
}
