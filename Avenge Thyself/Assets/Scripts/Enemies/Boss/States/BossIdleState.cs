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
        Debug.Log("IdleState");
        if (isHostile)
        {
            Debug.Log("IsHostile");
            //Do something

            var newState = ChaseState;

            isHostile = false;
            return newState;
        }
        else
        {
            Debug.Log("NotHostile");
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
        Debug.Log("Start waiting...");
        yield return new WaitForSeconds(restTime);
        Debug.Log("Finished waiting...");

        isHostile = true;
        waiting = false;
    }

}
