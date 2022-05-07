using UnityEngine;

public class BossDistanceAttackState : MonoBehaviour, State
{
    public BossIdleState IdleState;
    public Animator animator;
    public State RunState()
    {
        Debug.Log("Attack finished");
        animator.SetTrigger("cast");
        return IdleState;
    }
}