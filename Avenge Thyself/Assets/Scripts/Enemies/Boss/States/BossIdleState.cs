using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : MonoBehaviour, State
{
    public Rigidbody2D body;

    public Animator animator;

    public BossChaseState ChaseState;
    private bool isHostile;
    private bool waiting;
    public float restTime;

    private void Awake()
    {
        isHostile = false;
        waiting = false;
    }

    public State RunState()
    {
        if (isHostile)
        {
            //Do something

            var newState = ChaseState;

            isHostile = false;
            return newState;
        }
        else
        {
            if (!waiting)
            {
                StartCoroutine(Rest_Wait_Co());
            }
        }
        //Stay still, gravity applied
        body.velocity = Vector2.up * body.velocity;
        return this;
    }

    IEnumerator Rest_Wait_Co()
    {
        waiting = true;
        isHostile = false;
        yield return new WaitForSeconds(restTime);

        isHostile = true;
        waiting = false;
    }

}
