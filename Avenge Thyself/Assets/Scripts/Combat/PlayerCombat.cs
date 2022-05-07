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


    float timeLastAtt;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        attackStateMachine = GetComponent<AttackStateMachine>();
        isBlocking = false;
    }

    void OnBasicAttack()
    {
        float xDir = 0;
        //x,y
        //        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        Vector2 attPoint;

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
