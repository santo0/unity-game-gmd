using System.Collections;
using UnityEngine;
using System;

public class BossDistanceAttackState : MonoBehaviour, State
{
    public ShadowAttack shadowAttack;
    public BossIdleState IdleState;
    public Animator animator;
    public State RunState()
    {
        animator.SetTrigger("cast");
        StartCoroutine(StartAttack());
        return IdleState;
    }

    IEnumerator StartAttack()
    {
        shadowAttack.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        shadowAttack.gameObject.SetActive(false);
    }
}