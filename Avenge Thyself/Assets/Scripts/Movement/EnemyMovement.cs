using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{

    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private CircleCollider2D attHitBox;
    private GameObject hitBoxHandler;

    private Animator animator;

    private float heightDiff = 2;

    public float movSpeed = 2f;
    public float jumpSpeed = 10f;
    public bool canJump;
    public float attackRange = 4;
    public bool spriteFlipped;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attHitBox = GetComponentInChildren<CircleCollider2D>();
        hitBoxHandler = GetComponentInChildren<GameObject>();
        canJump = true;
        spriteFlipped = false;
    }



    public Transform GetTarget()
    {
        Debug.Log("GetTarget " + target);
        return target;
    }

    public void TargetPlayer(Transform player)
    {
        Debug.LogWarning("TargetPlayer " + player);
        target = player;
    }

    public void StopTarget()
    {
        Debug.LogWarning("StopTarget");
        target = null;
    }


    private void HorizontalMove(float dx)
    {
        //body.AddForce(new Vector2(dx, 0) * movSpeed);
        body.velocity = new Vector2(dx * movSpeed, body.velocity.y) * Time.deltaTime;
    }

    private void Jump()
    {
        Debug.Log("JUMPING MUSHROOM");
        body.AddForce(Vector2.up * jumpSpeed);
    }

    private void FixedUpdate()
    {
        //Debug.Log(tLock.GetTarget());
        if (target != null)
        {
            Vector2 dist = target.position - transform.position;
            var atRange = dist.magnitude <= attackRange;
            Debug.Log(atRange + " = " + dist.magnitude + " <= " + attackRange);
            animator.SetBool("Run", !atRange);

            if(spriteFlipped != (dist.normalized.x < 0)){
                spriteRenderer.flipX = !spriteRenderer.flipX;
                hitBoxHandler.transform.position = new Vector2(-1, 1) * hitBoxHandler.transform.position;
                spriteFlipped = !spriteFlipped;
            }



            if (atRange)
            {
                //Attack
                body.velocity = Vector2.up * body.velocity;
                animator.SetBool("Run", false);
            }
            else
            {
                if (canJump && dist.y > heightDiff)
                {
                    StartCoroutine(JumpCooldown());
                }
                HorizontalMove(dist.normalized.x);
            }
        }
        else
        {
            body.velocity = Vector2.up * body.velocity;
            animator.SetBool("Run", false);
        }
        //Mathf.Abs(body.velocity.x)

    }

    IEnumerator JumpCooldown()
    {
        canJump = false;
        Jump();
        yield return new WaitForSeconds(2f);
        canJump = true;
    }

}
