using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    public PlayerStats playerStats;
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
//        Debug.Log(enemy);
        HealthSys enemyHealthSystem = enemy.GetComponent<HealthSys>();
//        Debug.Log(enemyHealthSystem);
        enemyHealthSystem.TakeHit(playerStats.GetTotalDamage(), xDir);
        //        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir * 500f, 500f));
        //        damagePopupSpawner.SpawnDamagePopup(enemy.gameObject, dmg);
    }


}
