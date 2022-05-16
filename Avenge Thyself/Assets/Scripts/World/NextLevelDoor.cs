using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    public string nextLevelName;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollision");
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(Cor_EnterDoor());
        }
    }

    IEnumerator Cor_EnterDoor()
    {
        //Play door animation and load next level
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadLevel(nextLevelName);

    }
}
