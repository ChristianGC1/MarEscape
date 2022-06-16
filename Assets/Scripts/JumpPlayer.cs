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
    private Animator anim;
    private Vector2 lastCheckPoint;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public GameObject gameOverCanvas;
    public GameManager gameManager;
    public LayerMask groundMask;

    public Vector2 lastClickedPos;

    public bool moving;

    [Header("Jump Variables")]

    public int maxJump = 20;

    public int currentJump;

    public JumpBar jumpBar;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        FaceMouse();

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0f, groundMask);

        ManageJumpBar();

        //if (isGrounded)
        //{
        //    lastCheckPoint = transform.position + new Vector3(0, 1, 0);
        //}

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
            LastCheckPoint();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(walkSpeed, jumpValue);
                lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moving = true;
                jumpValue = 8f;
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
        jumpValue = 8;
    }

    void ResetMove()
    {
        if(isGrounded == true)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void LastCheckPoint()
    {
        rb.velocity = Vector3.zero;
        transform.position = lastCheckPoint;
        Time.timeScale = 1;
        gameOverCanvas.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(gizmosFeetA, gizmosFeetB));
    }

    void ManageJumpBar()
    {
        currentJump = Mathf.RoundToInt(jumpValue);
        jumpBar.SetJump(currentJump);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            lastCheckPoint = transform.position + new Vector3(0, 1, 0);
        }

        if (other.gameObject.CompareTag("Lose"))
        {
            gameManager.GameOver();
        }

        if (other.gameObject.CompareTag("Win"))
        {
            gameManager.YouWin();
        }
    }
}
