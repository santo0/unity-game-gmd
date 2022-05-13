using UnityEngine;
using System.Collections;
public class BossMeleeAttackState : MonoBehaviour, State
{
    public BossIdleState IdleState;
    public Animator animator;

    public BoxCollider2D dmgArea;

    public float attackDamage;

    public State RunState()
    {
        StartCoroutine(StartAttack());
        return IdleState;
    }


    IEnumerator StartAttack()
    {
        animator.SetTrigger("melee");
        yield return new WaitForSeconds(0.5f);
        foreach (Collider2D col in Physics2D.OverlapBoxAll(
            dmgArea.gameObject.transform.position,
            dmgArea.size * 10,
            LayerMask.NameToLayer("Player")))
        {
            Debug.Log(LayerMask.LayerToName(col.gameObject.layer));
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                bool isBlocking = col.gameObject.GetComponent<PlayerCombat>().IsBlocking();
                if (isBlocking)
                {
                    Debug.Log("BLOQUEJAT");
                }
                else
                {
                    Debug.Log("Hostion");
                    PlayerHealthSystem playerHS = col.gameObject.GetComponent<PlayerHealthSystem>();
                    var xDir = (
                        (col.gameObject.transform.position - gameObject.transform.position)
                            .normalized).x;
                    playerHS.TakeHit(attackDamage, xDir);
                }
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(dmgArea.gameObject.transform.position, dmgArea.size * 10);
    }
}