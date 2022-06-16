using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    //This player controller was created to simulate jump king mechanics
    public float walkSpeed;
    public bool canJump;
    public float jumpValue = 0.0f;
    public float gizmosFeetA;
    public float gizmosFeetB;

    private bool isGrounded;
    private Vector2 lastGroundedPos;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public LayerMask groundMask;

    public Vector2 lastClickedPos;

    public bool moving;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        FaceMouse();

        //Invoke("ResetMove", 0.1f);

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0f, groundMask);
        
        if (isGrounded)
        {
            lastGroundedPos = transform.position + new Vector3(0, 1, 0);
        }

        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Mouse0) && isGrounded && canJump)
        {
            jumpValue += 5.0f * 2.0f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (jumpValue >= 20f && isGrounded)
        {
            float tempx = walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.velocity = Vector3.zero;
            transform.position = lastGroundedPos;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(walkSpeed, jumpValue);
                lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moving = true;
                jumpValue = 5f;
            }

            if(moving && (Vector2)transform.position != lastClickedPos)
            {
                float step = walkSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            }
            else
            {
                moving = false;
            }
            canJump = true;
        }
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (mousePosition.x > transform.position.x)
        {
            //Player faces right
            walkSpeed = 5;
            sr.flipX = false;
        }
        else
        {
            //Player faces left
            walkSpeed = -5;
            sr.flipX = true;
        }
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 5;
    }

    void ResetMove()
    {
        if(isGrounded == true)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(gizmosFeetA, gizmosFeetB));
    }
}
