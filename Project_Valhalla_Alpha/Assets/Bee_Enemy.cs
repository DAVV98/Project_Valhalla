using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Enemy : MonoBehaviour
{
    [Header("Characteristics")]
    public float moveSpeed;
    public enum Bee_State { moveTowards, retreat };
    public Bee_State currentState;
    public float stingDist;
    public float maxDist;
    public float weight;

    //Privates
    private GameObject Player;
    private Transform Player_Pos;

    // Start is called before the first frame update
    void Start()
    {
        //Makes Player useable and gets its transfroms.
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Pos = Player.transform;

        //Start with approach state.
        currentState =  Bee_State.moveTowards;


    }

    // Update is called once per frame
    void Update()
    {
      
        switch (currentState)
        {
            case Bee_State.moveTowards:
                moveTowards();
            break;
            case Bee_State.retreat:
                retreat();
            break;
            
        }
    }

    //Make enemy move towards player
    void moveTowards()
    {
        //Look at player
        transform.LookAt(Player_Pos);

        //move towards players
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    void retreat()
    {
        //Look at player
        transform.LookAt(Player_Pos);

        //move towards players
        transform.position -= transform.forward * moveSpeed * Time.fixedDeltaTime;

        if (Vector3.Distance(Player_Pos.position, this.transform.position) >= stingDist)
        {
            currentState = Bee_State.moveTowards;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            currentState = Bee_State.retreat;
        }

        if (collision.tag == "Shield")
        {
            Debug.Log("Shield");
            Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
            Rigidbody rb = this.GetComponent<Rigidbody>();

            rb.AddForce(-(forceDirection * weight));
            currentState = Bee_State.moveTowards;
        }
    }

   
    void shieldPush()
    {
        Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(-(forceDirection * weight)); 
    }
}
