using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomIdleState : MonoBehaviour, State
{
    public NoiseReceiver noiseReceiver;
    public Rigidbody2D body;

    public Animator animator;

    public MushroomChaseState ChaseState;
    public State RunState()
    {
        Transform target = noiseReceiver.GetTarget();
        // check if target
        if (target != null)
        {
            return ChaseState;
        }
        else
        {
            body.velocity = Vector2.up * body.velocity;
            animator.SetBool("Run", false);
            return this;
        }
    }
}
