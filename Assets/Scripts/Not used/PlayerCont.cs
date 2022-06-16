using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    //This player controller was created to simulate jump king mechanics
    public float walkSpeed;
    private float moveInput;
    public bool canJump;
    public float jumpValue = 0.0f;

    private bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //FaceMouse();

        //Invoke("ResetMove", 0.1f);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0f, groundMask);

        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Mouse0) && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if(jumpValue >=20f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 5f;
            }
            canJump = true;
        }
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.right = direction;
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 5;
    }

    void ResetMove()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector3.zero;
        }

    }
}
