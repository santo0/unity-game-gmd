using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    public BoxCollider2D col;

    SpriteRenderer spriteRenderer;
    Animator animator;

    AttackStateMachine attackStateMachine;
    bool isBlocking;

    PlayerHealthSystem playerHealthSystem;


    float timeLastAtt;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        attackStateMachine = GetComponent<AttackStateMachine>();
        playerHealthSystem = GetComponent<PlayerHealthSystem>();
        isBlocking = false;
    }

    void OnBasicAttack()
    {
        if (playerHealthSystem.deadPlayer) return;
        if (isBlocking) return;

        Vector2 attPoint;
        float xDir = 0;

        if (spriteRenderer.flipX)
        {//Player looking at left

            attPoint = new Vector2(col.bounds.min.x, col.bounds.center.y);
            xDir = -1;
        }
        else
        { // Player looking at right
            attPoint = new Vector2(col.bounds.max.x, col.bounds.center.y);
            xDir = 1;
        }
        attackStateMachine.Attack(attPoint, 2f, xDir);
    }

    void OnBlock(InputValue value)
    {
        if (playerHealthSystem.deadPlayer) return;
        bool block = (value.Get<float>() == 1f);
        animator.SetBool("isBlocking", block);
        isBlocking = block;
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    void HitEnemy(Collider2D enemy, float xDir)
    {
        float dmg = 10;
        HealthSys enemyHealthSystem = enemy.GetComponent<HealthSys>();
        enemyHealthSystem.TakeHit(dmg, xDir);
        //        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir * 500f, 500f));
        //        damagePopupSpawner.SpawnDamagePopup(enemy.gameObject, dmg);
    }

    void Update()
    {
        timeLastAtt = Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
                    new Vector2(col.bounds.max.x, col.bounds.center.y),
                    2f);
        Gizmos.DrawWireSphere(
                            new Vector2(col.bounds.min.x, col.bounds.center.y),
                            2f);
    }
}
