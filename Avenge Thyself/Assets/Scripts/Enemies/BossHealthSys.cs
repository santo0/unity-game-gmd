using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSys : EnemyHealthSys
{

    public Healthbar healthbar;


    private void Start() {
        healthbar.SetMaxHealth(MAX_HP);
    }

    public override void Restart()
    {
        GameManager.instance.GoodGameOver();
        Destroy(gameObject);
    }

    protected override IEnumerator TakeDamage_Cor(float damage, float xDir)
    {
        Debug.LogWarning("Damaged!");
        hittable = false;
        hp -= damage;
        healthbar.SetHealth(hp);
        DamagePopupSpawner.instance.SpawnDamagePopup(gameObject, damage);
        animator.SetTrigger("DamageTaken");
        spriteRenderer.material.SetInt("_Hit", 1);

        if (hp <= 0.0f)
        {
            spriteRenderer.material.SetInt("_Hit", 0);
            Death();
            yield return new WaitForSeconds(DEAD_TIME);
            Restart();
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir * 500f, 500f));
            Debug.LogWarning("Hitted!");
            yield return new WaitForSeconds(PUSH_TIME);
            spriteRenderer.material.SetInt("_Hit", 0);
            hittable = true;
        }
    }


}
