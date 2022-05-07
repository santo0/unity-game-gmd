using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackState : MonoBehaviour, State
{
    public NoiseReceiver noiseReceiver;
    public Animator animator;

    public float attackRange = 4;
    public float damage = 10f;
    public MushroomIdleState IdleState;
    public MushroomChaseState ChaseState;

    public SpriteRenderer attackAlert;

    public CircleCollider2D punch;
    public CircleCollider2D bite;

    public BoxCollider2D body;

    private bool canAttack;

    private void Start()
    {
        canAttack = true;
    }

    IEnumerator StartAttack()
    {
        Debug.Log("ATTACK!");
        animator.SetTrigger("attack1");
        attackAlert.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackAlert.enabled = false;
        List<Collider2D> cols = new List<Collider2D>();
        cols.AddRange(Physics2D.OverlapCircleAll(punch.gameObject.transform.position, punch.radius));
        cols.AddRange(Physics2D.OverlapBoxAll(body.transform.position, body.size, LayerMask.NameToLayer("Player")));
        Debug.Log(body.transform.position + " como " + body.size);
        foreach (Collider2D col in cols)
        {
            Debug.Log(LayerMask.LayerToName(col.gameObject.layer));
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                bool isBlocking = col.gameObject.GetComponent<PlayerCombat>().IsBlocking();
                if (isBlocking)
                {
                    Debug.Log("BLOQUEJAT");
                }
                else
                {
                    Debug.Log("Hostion");
                    PlayerHealthSystem playerHS = col.gameObject.GetComponent<PlayerHealthSystem>();
                    var xDir = ((col.gameObject.transform.position - gameObject.transform.position).normalized).x;
                    playerHS.TakeHit(damage, xDir); //TODO: Implement direction calculation!!
                }
                break;
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f);
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
                StartCoroutine(AttackCooldown());
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
