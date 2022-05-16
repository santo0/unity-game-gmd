using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    public Animator animator;
    public string animationTrigger;

    public void StartCircleAttack(Vector2 attPoint, float radius, float xDir)
    {
        animator.SetTrigger(animationTrigger);
        var hitEnemies = Physics2D.OverlapCircleAll(attPoint, 2f);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                HitEnemy(enemy, xDir);
            }
        }
    }

    void HitEnemy(Collider2D enemy, float xDir)
    {
        HealthSys enemyHealthSystem = enemy.GetComponent<HealthSys>();
        enemyHealthSystem.TakeHit(GameManager.instance.playerStats.GetTotalDamage(), xDir);
    }


}
