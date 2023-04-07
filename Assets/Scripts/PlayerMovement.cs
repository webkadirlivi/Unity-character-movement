using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float horizontalInput;
    private float verticalInput;

    private Rigidbody2D rb;

    private Animator anim;

    private enum MovementState { idle, run, jump, fall }
    void Start()
    {
        //referans alma
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        MovementState state;
        // jump
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }

        // karakter yüzünü döndürme kontrol karakter hareket ediyor mu?
        if(horizontalInput > 0f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // animasyon kontrol karakter hareket ediyor mu?
        if(horizontalInput > 0f)
        {
            state = MovementState.run;
        }
       else if(horizontalInput < 0f)
        {
            state = MovementState.run;
        }
        else 
        {
            state = MovementState.idle;
        }

        //jump fall için

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }
        

        anim.SetInteger("state",(int)state);
    }

    private void FixedUpdate()
    {
        // input alma
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(horizontalInput , rb.velocity.y);
    }


}
