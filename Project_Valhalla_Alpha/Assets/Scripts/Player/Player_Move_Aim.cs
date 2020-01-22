using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Aim : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    /*
    [Header("Aiming")]
    //public float moveSpeed;
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
        mouseAim();
    }


    void movePlayer()
    {
        float input_horizontal = Input.GetAxisRaw("Horizontal");
        float input_vertical = Input.GetAxisRaw("Vertical");

        float x = input_horizontal * moveSpeed * Time.fixedDeltaTime;
        float y = 0.0f;
        float z = input_vertical * moveSpeed * Time.fixedDeltaTime;

        Vector3 newPosition = new Vector3(x, y, z);
        transform.position = transform.position + newPosition;
    }

    void mouseAim()
    {
        //Position of Object
        Vector2 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        //Position of Mouse
        Vector2 mousePos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition + Vector3.back * 10f);

        //Object -> Mouse Angle
        float angle = AngleBetweenTwoPoints(screenPos, mousePos);

        //Rotate player
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y- b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
