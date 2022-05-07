using UnityEngine;

public class BossMeleeAttackState : MonoBehaviour, State
{
    public BossIdleState IdleState;
    public Animator animator;
    public State RunState()
    {
        Debug.Log("Attack finished");
        animator.SetTrigger("melee");
        return IdleState;
    }
}