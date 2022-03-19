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


    private void Awake()
    {
        body =  GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        canJump = true;
    }


    private void OnMove(InputValue value) {
        Debug.Log(value.Get<Vector2>());        
        dir = value.Get<Vector2>();
    }

    private void OnJump(InputValue value) {
        float val = value.Get<float>();
        if (val == 1f){
            jumpPressed = true;
        } else {
            jumpPressed = false;
            canJump = false;
        }
        Debug.Log(jumpPressed +" --- "+ val);
    }   


    private void FixedUpdate() {
        if(dir.x < 0) {
            Debug.Log("Left");
            spriteRenderer.flipX = true;
        }else if(dir.x > 0){
            Debug.Log("Right");
            spriteRenderer.flipX = false;
        }
        float xDir = dir.x * movSpeed * Time.deltaTime;
        float yDir = body.velocity.y;
        if (jumpPressed && colDetect.isPlayerGrounded()) {
            Debug.Log("Up");
            yDir += jumpSpeed;
            jumpTimeCounter = jumpTime;
            canJump = true;
        }

        body.velocity = new Vector2(xDir, yDir);

        if(jumpPressed && canJump){
            if (jumpTimeCounter > 0){
                body.velocity = body.velocity + Vector2.up * jumpSpeed * (jumpTimeCounter/jumpTime);
                jumpTimeCounter -= Time.deltaTime;
            } else {
                jumpPressed = false;
            }
        }

        if(colDetect.isPlayerGrounded()){
            animator.SetBool("isGrounded", true);
        }else{
            animator.SetBool("isGrounded", false);
        }
        animator.SetFloat("speedX", Mathf.Abs(body.velocity.x));
        animator.SetFloat("speedY", body.velocity.y);
        dir = new Vector2(dir.x, 0f);
    }
}
