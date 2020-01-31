using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Aim : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [Header("Aim")]
    public float rotateSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
        mouseAim();
        //arrowsAim();
    }


    void movePlayer()
    {
        //Get Axis
        float input_horizontal = Input.GetAxisRaw("Horizontal_WASD");
        float input_vertical = Input.GetAxisRaw("Vertical_WASD");

        //Create x,y,z movement.
        float x = input_horizontal * moveSpeed * Time.fixedDeltaTime;
        float y = 0.0f;
        float z = input_vertical * moveSpeed * Time.fixedDeltaTime;

        // use movement to create a new vector.
        Vector3 newPosition = new Vector3(x, y, z);

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

    void arrowsAim()
    {
        /*
        //get Axis
    
        float yRotation = Input.GetAxisRaw("Horizontal_Arrows");

        //create x,y,z rotation
        float yRotate = yRotation * rotateSpeed * Time.fixedDeltaTime;

        //rotate vector
        Vector3 rotate_player = new Vector3(0, yRotate, 0);

        //aim player
        transform.Rotate(rotate_player);
        */

        /*
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal_Arrows"), 0, Input.GetAxis("Vertical_Arrows"));
        Vector3 realDirection = Camera.main.transform.TransformDirection(movementDirection);
        // this line checks whether the player is making inputs.
        if (realDirection.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(realDirection);
            transform.rotation = newRotation;
        }
        */
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y- b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
