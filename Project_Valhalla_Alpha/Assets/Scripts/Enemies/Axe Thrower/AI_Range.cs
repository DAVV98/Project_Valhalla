using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Range : MonoBehaviour
{
    [Header("Target")]
    public Transform Player;

    [Header("Characteristics")]
    public int moveSpeed;
    public int maxDist;
    public int minDist;

    // Update is called once per frame
    void FixedUpdate()
    {
        Range_AI();
    }

    void Range_AI()
    {
        transform.LookAt(Player);

        if(Vector3.Distance(transform.position, Player.position) > minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
        }
        else if (Vector3.Distance(transform.position, Player.position) < maxDist)
        {
            transform.position -= transform.forward * moveSpeed * Time.fixedDeltaTime;
        }

        //TODO make it so that enemy isnt jagged when at minDist.
    }
}
