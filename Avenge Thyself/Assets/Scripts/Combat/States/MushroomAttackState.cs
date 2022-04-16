using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackState : MonoBehaviour, State
{
    public NoiseReceiver noiseReceiver;
    public Animator animator;

    public float attackRange = 4;

    public MushroomIdleState IdleState;
    public MushroomChaseState ChaseState;

    public CircleCollider2D punch;
    public CircleCollider2D bite;

    private bool canAttack;

    private void Start() {
        canAttack = true;
    }

    IEnumerator StartAttack()
    {
        canAttack = false;
        Debug.Log("ATTACK!");
        animator.SetTrigger("attack1");

        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    public State RunState()
    {
        Transform target = noiseReceiver.GetTarget();
        if (target != null)
        {
            Vector2 dist = target.position - transform.position;
            var atRange = dist.magnitude <= attackRange;
            if (atRange && canAttack)
            {
                //attack
                StartCoroutine(StartAttack());
                return this;
            }
            else
            {
                return ChaseState;
            }
        }
        else
        {
            return IdleState;
        }
    }
}
