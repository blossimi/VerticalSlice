using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public GameObject Player;
    private GameObject playerSprite;
    private Rigidbody2D rb;
    public float movementSpeed;
    public bool movementLocked;
    public ExController controls;
    private Animator anim;
    private float offset = 0.1f;
    private void Awake()
    {
        controls = new ExController();
    }
    private void Start()
    {
        playerSprite = GameObject.Find("player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = playerSprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (!movementLocked){
            if (Input.GetKey(KeyCode.A)) // || controls.Gameplay.MoveLeft.performed)
            {

                transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
                anim.SetBool("MoveLeft", true);
                playerSprite.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            }
            else
            {
                playerSprite.transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
                anim.SetBool("MoveLeft", false);
            }

            if (Input.GetKey(KeyCode.D))
            {

                transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
                anim.SetBool("MoveRight", true);
                playerSprite.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            }
            else
            {
                playerSprite.transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
                anim.SetBool("MoveRight", false);
            }

            if (Input.GetKey(KeyCode.W))
            {

                transform.position += new Vector3(0, 0, movementSpeed) * Time.deltaTime;
                anim.SetBool("MoveUp", true);
                playerSprite.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            }
            else
            {
                playerSprite.transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
                anim.SetBool("MoveUp", false);
            }

            if (Input.GetKey(KeyCode.S))
            {

                transform.position += new Vector3(0, 0, -movementSpeed) * Time.deltaTime;
                anim.SetBool("MoveDown", true);
                playerSprite.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            }
            else
            {
                playerSprite.transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
                anim.SetBool("MoveDown", false);
            }
        }
    }







}
