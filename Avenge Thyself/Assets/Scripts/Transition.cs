using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    public Animator animator;
    public float transitionTime = 1f;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

}
