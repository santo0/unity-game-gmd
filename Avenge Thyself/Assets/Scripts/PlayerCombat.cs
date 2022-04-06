using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public BoxCollider2D col;

    public SpriteRenderer spriteRenderer;

    public Animator animator;
    public HealthSystem hs;
    enum AttackType
    {
        NoAttack,
        Attack1,
        Attack2,
        Attack3
    }
    private AttackType lastAtt;
    private float timeLastAtt;

    void Awake()
    {
        lastAtt = AttackType.NoAttack;
    }

    void OnBasicAttack()
    {
        float xDir = 0;
        Debug.Log("I ATTACKED");
        //x,y
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //Left
        Collider2D[] hitEnemies = { };
        if (mousePos.x < 0.5)
        {
            spriteRenderer.flipX = true;
            hitEnemies = Physics2D.OverlapCircleAll(
                new Vector2(col.bounds.min.x, col.bounds.center.y),
                2f);
            xDir = -1;
        }
        //Right
        else
        {
            spriteRenderer.flipX = false;
            hitEnemies = Physics2D.OverlapCircleAll(
                new Vector2(col.bounds.max.x, col.bounds.center.y),
                2f);
            xDir = 1;
        }
        animator.SetTrigger("attack1");
        lastAtt = AttackType.Attack1;

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Something hitted: " + enemy);
            if(enemy.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                HealthSystem enemyHealthSystem = enemy.GetComponent<HealthSystem>();
                enemyHealthSystem.TakeDamage(10f);
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir * 500f, 500f));
                Debug.Log("ENEMY!!!! 10 damage fuu");
            }
        }
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
