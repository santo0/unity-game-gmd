using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float MAX_VEL = 10f;
    public Vector2 HZ_JUMP;
    public Vector2 VT_JUMP;

    public CollisionDetection colDetect;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private PlayerOneWayPlatformController oneWayPlatformController;

    PlayerHealthSystem playerHealthSystem;
    PlayerCombat playerCombat;

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
        oneWayPlatformController = GetComponent<PlayerOneWayPlatformController>();
        playerHealthSystem = GetComponent<PlayerHealthSystem>();
        playerCombat = GetComponent<PlayerCombat>();
    }
    private void OnMove(InputValue value)
    {

        if (playerHealthSystem.deadPlayer || playerCombat.IsBlocking())
        {
            dir = Vector2.zero;
            return;
        }

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
        if (playerHealthSystem.deadPlayer || playerCombat.IsBlocking())
        {
            jumpPressed = false;
            return;
        }

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
        //player input move left or right
        else if (dir.x != 0)
        {
            horizontalWallJump();
        }
        else
        {
            //Give friction effect if wallsliding and going down
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
            //Stay in position
            animator.SetBool("isOnLedge", true);
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
            //physics don't apply / no gravity
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

        if (body.velocity.x > -MAX_VEL && body.velocity.x < MAX_VEL)
        {
            //physics apply
            body.isKinematic = false;
            body.AddForce(dir * movSpeed * Time.deltaTime);
        }
        if (jumpPressed)
        {
            //physics apply
            body.isKinematic = false;
            body.AddForce(Vector2.up * jumpSpeed);
            jumpTimeCounter = jumpTime;
        }
    }
    private void horizontalWallJump()
    {
        //resets Y-axis velocity
        body.velocity = body.velocity + Vector2.down * body.velocity;

        //Vector2 jForce = new Vector2(300f, 700f);
        Vector2 jForce = HZ_JUMP;
        if (colDetect.isCollBotRight() && dir.x < 0)
        {
            body.isKinematic = false;
            body.AddForce(jForce * new Vector2(-1, 1));
            spriteRenderer.flipX = true;
        }
        else if (colDetect.isCollBotLeft() && dir.x > 0)
        {
            body.isKinematic = false;
            body.AddForce(jForce);
            spriteRenderer.flipX = false;
        }
    }

    private void verticalWallJump()
    {
        //resets Y-axis velocity (problems with the momentum)
        body.velocity = body.velocity + Vector2.down * body.velocity;
        //Vector2 jForce = new Vector2(0f, 1000f);
        Vector2 jForce = VT_JUMP;
        body.isKinematic = false;
        body.AddForce(jForce);
    }

    private void airInteraction()
    {
        // physics apply
        body.isKinematic = false;

        //speed cap
        if (-MAX_VEL < body.velocity.x && body.velocity.x < MAX_VEL)
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

    private void OnDown()
    {
        if (oneWayPlatformController.currentOneWayPlatform != null)
        {
            StartCoroutine(oneWayPlatformController.Cor_DisableCollision());
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
