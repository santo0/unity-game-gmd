using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public float maxHp = 100f;
    
    [SerializeField]
    private  float hp;
    public Animator animator;


    private void Awake() {
        hp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        animator.SetTrigger("DamageTaken");
        if (hp <= 0.0f)
        {
            CharacterDeath();
        }
    }

    public abstract void CharacterDeath();



}
