using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public float MAX_HP = 100f;

    [SerializeField]
    protected float hp;
    protected Animator animator;

    SpriteRenderer spriteRenderer;

    protected bool hittable;
    public float PUSH_TIME = 0.5f;
    public float DEAD_TIME = 2f;

    public bool isHittable()
    {
        return hittable;
    }

    private void Awake()
    {
        hp = MAX_HP;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hittable = true;
    }

    public void TakeDamage(float damage)
    {
        IEnumerator TakeDamage_Cor()
        {
            Debug.LogWarning("Damaged!");
            hittable = false;
            hp -= damage;
            animator.SetTrigger("DamageTaken");
            spriteRenderer.material.SetInt("_Hit", 1);

            if (hp <= 0.0f)
            {
                CharacterDeath();
                yield return new WaitForSeconds(DEAD_TIME);
                spriteRenderer.material.SetInt("_Hit", 0);
                CharacterRestart();
            }
            else
            {
                Debug.LogWarning("Hitted!");
                yield return new WaitForSeconds(PUSH_TIME);
                spriteRenderer.material.SetInt("_Hit", 0);
            }
            hittable = true;
        }
        if (hittable)
        {
            StartCoroutine(TakeDamage_Cor());
        }

    }

    public abstract void CharacterDeath();
    public abstract void CharacterRestart();



}
