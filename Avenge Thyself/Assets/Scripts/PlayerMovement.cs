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

    private bool canJump;

    [SerializeField]
    private Vector2 forces;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        canJump = true;
    }


    private void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        dir = value.Get<Vector2>();
        if (dir.x < 0)
        {
            Debug.Log("Left");
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            Debug.Log("Right");
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
            canJump = false;
        }
        Debug.Log(jumpPressed + " --- " + val);
    }


    private void wallInteraction()
    {
        //touching right or left wall
        if (colDetect.checkIfWall())
        {

            if (colDetect.isCollBotLeft())
            {
                spriteRenderer.flipX = true;
            }
            else if (colDetect.isCollBotRight())
            {
                spriteRenderer.flipX = false;
            }


            animator.SetBool("touchWall", true);


            //player input jump and not grounded
            if (jumpPressed && !colDetect.isPlayerGrounded())
            {
                horizontalWallJump();
            }
            else
            {
                //Wall slide with friction
                if (body.velocity.y < 0f)
                {
                    Vector2 wallForce = (-body.velocity) * body.mass * body.gravityScale * Vector2.up;
                    Debug.Log("wallForce = " + wallForce);
                    body.AddForce(wallForce);
                    //newVelocity = newVelocity + Vector2.up * 0.2f; //ficar 0.2 en variable, friction o algo
                }
            }

        }
        else
        {
            //Is not touching a wall
            animator.SetBool("touchWall", false);
        }
    }

    private void ledgeInteraction()
    {
        //touching right or left wall
        if (colDetect.checkIfLedge())
        {
            if (jumpPressed)
            {
                body.isKinematic = false;
                verticalWallJump();
            }
            else if(dir.x != 0f) {
                body.isKinematic = false;
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
        else
        {
            //Is not touching a wall
            animator.SetBool("isOnLedge", false);
            body.isKinematic = false;
        }
    }

    private void horizontalWallJump()
    {
        //resets Y-axis velocity (problems with the momentum)
        body.velocity = body.velocity + Vector2.down * body.velocity;
        if (colDetect.isCollBotRight())
        {
            body.AddForce(new Vector2(-300f, 700f));
        }
        else
        {
            body.AddForce(new Vector2(300f, 700f));
        }
    }

    private void verticalWallJump()
    {
        //resets Y-axis velocity (problems with the momentum)
        body.velocity = body.velocity + Vector2.down * body.velocity;
        body.AddForce(new Vector2(0f, 1000f));        
    }

    private void FixedUpdate()
    {
        Vector2 newVelocity = Vector2.zero;
        //newVelocity = dir * movSpeed * Time.deltaTime + Vector2.up * body.velocity.y;
        //player input lateral movement
        if (body.velocity.x > -10 && body.velocity.x < 10f)
        {
            body.AddForce(dir * movSpeed * Time.deltaTime);
        }

        //player input jump pressed
        if (jumpPressed)
        {
            //player touching ground -> always can jump
            if (colDetect.isPlayerGrounded())
            {
                Debug.Log("Up");
                //newVelocity = newVelocity + Vector2.up * jumpSpeed;
                body.AddForce(Vector2.up * jumpSpeed);
                jumpTimeCounter = jumpTime;
                canJump = true;
                //player not touching ground and can jump
            }
            else if (canJump)
            {
                //while counter active
                if (jumpTimeCounter > 0)
                {
                    //                newVelocity = newVelocity + Vector2.up*jumpSpeed*(jumpTimeCounter/jumpTime);
                    body.AddForce(Vector2.up * jumpSpeed * (jumpTimeCounter / jumpTime));
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    jumpPressed = false;
                }
            }
        }

        //touching ground animator
        if (colDetect.isPlayerGrounded())
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }

        wallInteraction();

        ledgeInteraction();

        //body.velocity = newVelocity;
        animator.SetFloat("speedX", Mathf.Abs(body.velocity.x));
        animator.SetFloat("speedY", body.velocity.y);

        forces = body.velocity;
        dir = dir * Vector2.right;
    }
}
