using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public float maxHp = 100f;

    [SerializeField]
    private float hp;
    protected Animator animator;

    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        hp = maxHp;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        IEnumerator TakeDamage_Cor()
        {
            hp -= damage;
            animator.SetTrigger("DamageTaken");
            spriteRenderer.material.SetInt("_Hit", 1);
            if (hp <= 0.0f)
            {
                CharacterDeath();
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                spriteRenderer.material.SetInt("_Hit", 0);
            }
        }
        StartCoroutine(TakeDamage_Cor());
    }

    public abstract void CharacterDeath();



}
