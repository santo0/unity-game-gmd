using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomChaseState : MonoBehaviour, State
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;

    public Transform attacksTransform;

    public Animator animator;

    public float movSpeed = 2f;
    public float attackRange = 4;
    public bool spriteFlipped = false;

    public MushroomIdleState IdleState;
    public MushroomAttackState AttackState;
    public NoiseReceiver noiseReceiver;
    public EnemyHealthSystem enemyHealthSystem;

    private void HorizontalMove(float dx)
    {
        //body.AddForce(new Vector2(dx, 0) * movSpeed);
        body.velocity = new Vector2(dx * movSpeed * Time.deltaTime, body.velocity.y);
    }
    public State RunState()
    {
        Transform target = noiseReceiver.GetTarget();
        //if there is a target, and mushroom can be hit (not invencible)
        if (target != null && enemyHealthSystem.isHittable())
        {
            Vector2 dist = target.position - transform.position;
            var atRange = dist.magnitude <= attackRange;
            //Debug.Log(atRange + " = " + dist.magnitude + " <= " + attackRange);
            animator.SetBool("Run", !atRange);

            if (spriteFlipped != (dist.normalized.x < 0))
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                attacksTransform.localScale = new Vector3(-attacksTransform.localScale.x,
                                                          attacksTransform.localScale.y,
                                                          attacksTransform.localScale.z);
                spriteFlipped = !spriteFlipped;
            }

            if (atRange)
            {
                return AttackState;
            }
            else
            {
                HorizontalMove(dist.normalized.x);
                return this;
            }
        }
        else
        {
            return IdleState;
        }
    }
}
