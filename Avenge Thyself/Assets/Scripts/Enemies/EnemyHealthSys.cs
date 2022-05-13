using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSys : MonoBehaviour, HealthSys
{

    public float MAX_HP = 100f;

    [SerializeField]
    protected float hp;
    protected Animator animator;

    protected SpriteRenderer spriteRenderer;
    protected bool hittable;
    public float PUSH_TIME = 0.5f;
    public float DEAD_TIME = 2f;
    private void Awake()
    {
        hp = MAX_HP;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hittable = true;
    }

    public void TakeHit(float damage, float xDir)
    {
        if (hittable)
        {
            StartCoroutine(TakeDamage_Cor(damage, xDir));
        }
    }

    public void Death()
    {
        Debug.LogWarning("Death of enemy animation");
        animator.SetTrigger("Death");
    }

    virtual public void Restart()
    {
        //update gamedatabase
        Destroy(gameObject);
    }

    public bool IsHittable()
    {
        return hittable;
    }


    protected virtual IEnumerator TakeDamage_Cor(float damage, float xDir)
    {
        Debug.LogWarning("Damaged!");
        hittable = false;
        hp -= damage;
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

    public float GetHP()
    {
        return hp;
    }


}
