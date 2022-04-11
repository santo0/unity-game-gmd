using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackState : MonoBehaviour, State
{
    public NoiseReceiver noiseReceiver;

    public float attackRange = 4;

    public MushroomIdleState IdleState;
    public MushroomChaseState ChaseState;

    IEnumerator StartAttack(){
        Debug.Log("ATTACK!");
        yield return new WaitForSeconds(1f);
    }
    public State RunState()
    {
        Transform target = noiseReceiver.GetTarget();
        if (target != null)
        {
            Vector2 dist = target.position - transform.position;
            var atRange = dist.magnitude <= attackRange;
            if (atRange)
            {
                //attack
                StartCoroutine(StartAttack());
                return this;
            }
            else
            {
                return ChaseState;
            }
        }
        else
        {
            return IdleState;
        }
    }
}
