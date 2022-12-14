using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rb;
    public float movementSpeed;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.A))
        {

            transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {

            transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {

            transform.position += new Vector3(0, 0, movementSpeed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {

            transform.position += new Vector3(0, 0, -movementSpeed) * Time.deltaTime;
        }
        //
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {

            transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {

            transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {

            transform.position += new Vector3(0, 0, movementSpeed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {

            transform.position += new Vector3(0, 0, -movementSpeed) * Time.deltaTime;
        }




    }







}
