using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public State currentState;

    public StateLoader stateLoader;

    public EnemyHealthSys healthSys;


    private void Awake()
    {
        currentState = stateLoader.LoadInitialState();
    }

    void FixedUpdate()
    {
        if (healthSys.GetHP() > 0)
        {
            State nextState = currentState.RunState();
            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
