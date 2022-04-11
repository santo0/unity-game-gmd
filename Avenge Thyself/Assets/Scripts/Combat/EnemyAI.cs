using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public State currentState;

    private void Awake() {
        currentState = GetComponentInChildren<MushroomIdleState>();
        Debug.Log(currentState);
    }

    void FixedUpdate()
    {
        State nextState = currentState.RunState();
        if (nextState != null)
        {
            currentState = nextState;
        }
    }
}
