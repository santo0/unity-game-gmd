using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAttack : MonoBehaviour
{

    //Steps
    //Awake-Start->Hidden
    //Call StartAttack->Shows in given position (on top of player)
    //Multile animations->Show area->Start damage area->...
    //Hide

    public GameObject player; //Attacks on top of player

    public Animator animator;

    public BoxCollider2D dmgArea;

    public float attackDamage;


    private void OnEnable()
    {
        Attack();
    }

    public void Attack()
    {
        var playerPosition = player.gameObject.transform.position;
        gameObject.transform.position = playerPosition + new Vector3(0, 2, 0);
        StartCoroutine(Co_Attack());
    }

    IEnumerator Co_Attack()
    {
        animator.SetTrigger("Show");
        //wait for finish
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Attacking());
        //bool damageOn = true;
        //if player enters area, is damaged
        //when damage player, can't damage again

    }

    IEnumerator Attacking()
    {
        animator.SetTrigger("Start");
        StartAttack();
        yield return new WaitForSeconds(0.5f);
    }


    private void StartAttack()
    {
        foreach (Collider2D col in Physics2D.OverlapBoxAll(transform.position, dmgArea.size, LayerMask.NameToLayer("Player")))
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

}
