﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_AI : MonoBehaviour
{

    [Header("Characteristics")]
    public float moveSpeed;
    public float escapeSpeed;
    public float stopPos;
    public float retreatDistance;
    public enum Axe_State { Approach, Throw, Retreat };
    public Axe_State currentState;
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


    // Start is called before the first frame update
    void Start()
    {
        //Makes Player useable and gets its transfroms.
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Pos = Player.transform;

        //Start with approach state.
        currentState = Axe_State.Approach;

         //Set up timer.
        time_between_shots = start_time_between_shots - 0.8f;

        thrown_axes = 0;
    }

    // Update is called once per frame
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
        }

      
    }

    void Approach()
    {
        //look at player
        transform.LookAt(Player_Pos);
        //move towards players
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
        //start throw.
        Attack_Timer();
        //thrown axe goes towareds player.
        axeObject.transform.LookAt(Player_Pos);

        //Debug.Log(thrown_axes);
        
        //Change state to retreat.
        if (thrown_axes == max_Axes)
        {
            currentState = Axe_State.Retreat;
        }
    }

    void Retreat()
    {
        //look at player
        transform.LookAt(Player_Pos);
        //move towards players
        transform.position -= transform.forward * (moveSpeed + escapeSpeed) * Time.fixedDeltaTime;

        //Stop at escape distance and start approach again.
        if (Vector3.Distance(Player_Pos.position, this.transform.position) >= retreatDistance)
        {
            currentState = Axe_State.Approach;
        }
    }
    void Attack_Timer()
    {
        if (time_between_shots <= 0)
        {
            Instantiate(axeObject, axeSpawn.position, axeSpawn.rotation);
            thrown_axes++;
            time_between_shots = start_time_between_shots;
        }
        else
        {
            time_between_shots -= Time.fixedDeltaTime;
        }
    }
}