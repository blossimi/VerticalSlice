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
    private GameObject spriteParent;
    
    private void Awake()
    {
        controls = new ExController();
    }
    private void Start()
    {
        playerSprite = GameObject.Find("player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = playerSprite.GetComponent<Animator>();
        spriteParent = transform.Find("spriteparent").gameObject;
    }

    void Update()
    {
        if (!movementLocked){
            if (Input.GetKey(KeyCode.A)) // || controls.Gameplay.MoveLeft.performed)
            {

                transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
                anim.SetBool("MoveLeft", true);
                
                
            }
            else
            {
                anim.SetBool("MoveLeft", false);
            }

            if (Input.GetKey(KeyCode.D))
            {

                transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
                anim.SetBool("MoveRight", true);
            }
            else
            {
                anim.SetBool("MoveRight", false);
            }

            if (Input.GetKey(KeyCode.W))
            {

                transform.position += new Vector3(0, 0, movementSpeed) * Time.deltaTime;
                anim.SetBool("MoveUp", true);
            }
            else
            {
                anim.SetBool("MoveUp", false);
            }

            if (Input.GetKey(KeyCode.S))
            {

                transform.position += new Vector3(0, 0, -movementSpeed) * Time.deltaTime;
                anim.SetBool("MoveDown", true);
            }
            else
            {
                anim.SetBool("MoveDown", false);

            }
        }
    }







}
