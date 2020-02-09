using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_AI : MonoBehaviour
{

    [Header("Characteristics")]
    public int moveSpeed;
    public float stopPos;
    public enum Axe_State { Approach, Throw, Retreat };
    public Axe_State currentState;
    private GameObject Player;
    private Transform Player_Pos;


    [Header("Axe Throw")]
    public GameObject axeObject;
    public Transform axeSpawn;
    public float throwSpeed;
    private float time_between_shots;
    public float start_time_between_shots;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Pos = Player.transform;

        currentState = Axe_State.Approach;

          //Set up timer.
        time_between_shots = start_time_between_shots;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case Axe_State.Approach:
                Approach();
                break;
            case Axe_State.Throw:
                Throw();
                break;
            case Axe_State.Retreat:
                break;
        }

      
    }

    void Approach()
    {
        //look at player
        transform.LookAt(Player_Pos);
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;

        if (Vector3.Distance(Player_Pos.position, this.transform.position) <= stopPos)
        {
            currentState = Axe_State.Throw;
        }
    }

    void Throw()
    {
        transform.LookAt(Player_Pos);
        Attack_Timer();
        axeObject.transform.LookAt(Player_Pos);
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
            time_between_shots -= Time.fixedDeltaTime;
        }
    }
}
