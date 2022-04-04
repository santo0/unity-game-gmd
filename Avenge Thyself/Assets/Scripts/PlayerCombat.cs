using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
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
        Debug.Log("I ATTACKED");
        //x,y
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //Left
        if (mousePos.x < 0.5)
        {
            spriteRenderer.flipX = true;
        }
        //Right
        else
        {
            spriteRenderer.flipX = false;
        }
        animator.SetTrigger("attack1");
        lastAtt = AttackType.Attack1;
    }

    void Update()
    {
        timeLastAtt = Time.deltaTime;
    }
}
