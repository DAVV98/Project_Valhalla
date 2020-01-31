using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Aim : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [Header("Aim")]
    public float rotateSpeed;


    // Update is called once per frame
    void FixedUpdate()
    {
        move_aim_Player();
        //arrowsAim();
    }


    void move_aim_Player()
    {
        //Get Axis
        float input_horizontal = Input.GetAxisRaw("Horizontal");
        float input_vertical = Input.GetAxisRaw("Vertical");

        //Create x,y,z movement.
        float x = input_horizontal * moveSpeed * Time.fixedDeltaTime;
        float y = 0.0f;
        float z = input_vertical * moveSpeed * Time.fixedDeltaTime;

        // use movement to create a new vector.
        Vector3 newPosition = new Vector3(x, y, z);

        // Look at move direction, and stay looking when not pressing.
        if (input_horizontal == -1 || input_horizontal == 1 || input_vertical == -1 || input_vertical == 1)
        {
            Vector3 movement = new Vector3(input_horizontal, 0.0f, input_vertical);
            transform.rotation = Quaternion.LookRotation(movement);
        }

        //use vector to move player.
        transform.position = transform.position + newPosition;
    }

    void mouseAim()
    {
        /*
          gamesplusjames (2016). Available at: https://www.youtube.com/watch?v=lkDGk3TjsIE (Accessed: 31 Jan 2020).
        */

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, this.transform.position.y, pointToLook.z));
        }
        
      
    }
     
}
