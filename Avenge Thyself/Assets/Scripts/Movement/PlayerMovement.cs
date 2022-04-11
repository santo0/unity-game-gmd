using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CollisionDetection colDetect;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    public Animator animator;
    private Vector2 dir;
    public float movSpeed = 400f;
    public float jumpSpeed = 2f;

    private bool jumpPressed;
    private float jumpTimeCounter;
    public float jumpTime = 0.2f;

    [SerializeField]
    private Vector2 forces;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnMove(InputValue value)
    {
        dir = value.Get<Vector2>();
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnJump(InputValue value)
    {
        float val = value.Get<float>();
        if (val == 1f)
        {
            jumpPressed = true;
        }
        else
        {
            jumpPressed = false;
        }
    }


    private void wallInteraction()
    {

        if (colDetect.isCollBotLeft())
        {
            spriteRenderer.flipX = true;
        }
        else if (colDetect.isCollBotRight())
        {
            spriteRenderer.flipX = false;
        }

        //player input jump and not grounded
        if (jumpPressed && !colDetect.isPlayerGrounded())
        {
            horizontalWallJump();
        }
        else if (dir.x != 0)
        {
            horizontalWallJump();
        }
        else
        {
            //Wall slide with friction
            if (body.velocity.y < 0f)
            {
                Vector2 wallForce = (-body.velocity) * body.mass * body.gravityScale * Vector2.up;
                body.AddForce(wallForce);
            }
        }


    }

    private void ledgeInteraction()
    {
        //touching right or left wall
        if (jumpPressed)
        {
            verticalWallJump();
        }
        else if (dir.x != 0f)
        {
            horizontalWallJump();
        }
        else
        {
            animator.SetBool("isOnLedge", true);
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
            body.isKinematic = true;
            if (colDetect.isCollBotLeft())
            {
                spriteRenderer.flipX = true;
            }
            else if (colDetect.isCollBotRight())
            {
                spriteRenderer.flipX = false;
            }
        }

    }

    private void groundInteraction()
    {

        if (body.velocity.x > -10 && body.velocity.x < 10f)
        {
            body.isKinematic = false;
            body.AddForce(dir * movSpeed * Time.deltaTime);
        }
        if (jumpPressed)
        {
            body.isKinematic = false;
            body.AddForce(Vector2.up * jumpSpeed);
            jumpTimeCounter = jumpTime;
        }
    }
    private void horizontalWallJump()
    {
        //resets Y-axis velocity (problems with the momentum)
        body.velocity = body.velocity + Vector2.down * body.velocity;

        Vector2 jForce = new Vector2(300f, 700f);
        if (colDetect.isCollBotRight() && dir.x < 0)
        {
            body.isKinematic = false;
            body.AddForce(jForce * new Vector2(-1, 1));
        }
        else if (colDetect.isCollBotLeft() && dir.x > 0)
        {
            body.isKinematic = false;
            body.AddForce(jForce);
        }
    }

    private void verticalWallJump()
    {
        //resets Y-axis velocity (problems with the momentum)
        body.velocity = body.velocity + Vector2.down * body.velocity;
        Vector2 jForce = new Vector2(0f, 1000f);
        body.isKinematic = false;
        body.AddForce(jForce);
    }

    private void airInteraction()
    {
        // solves bug with ledge
        body.isKinematic = false;

        if (body.velocity.x > -10 && body.velocity.x < 10f)
        {
            body.AddForce(dir * movSpeed * Time.deltaTime);
        }
        if (jumpPressed)
        {
            //while counter active
            if (jumpTimeCounter > 0)
            {
                body.AddForce(Vector2.up * jumpSpeed * (jumpTimeCounter / jumpTime));
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumpPressed = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (colDetect.isPlayerGrounded())
        {
            groundInteraction();
        }
        else if (colDetect.isPlayerOnLedge())
        {
            ledgeInteraction();
        }
        else if (colDetect.isPlayerAtWall())
        {
            wallInteraction();
        }
        else
        {
            airInteraction();
        }

        animator.SetBool("isGrounded", colDetect.isPlayerGrounded());
        animator.SetBool("isOnLedge", colDetect.isPlayerOnLedge());
        animator.SetBool("touchWall", colDetect.isPlayerAtWall());
        animator.SetFloat("speedX", Mathf.Abs(body.velocity.x));
        animator.SetFloat("speedY", body.velocity.y);

        forces = body.velocity;
        dir = dir * Vector2.right;
    }
}
