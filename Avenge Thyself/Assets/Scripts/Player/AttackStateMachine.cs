using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachine : MonoBehaviour
{

    [SerializeField]
    public Attack[] attacks;

    [SerializeField]
    private int attackIndex;

    [SerializeField]
    private bool canAttack;


    private void Awake()
    {
        canAttack = true;
        attackIndex = 0;
    }


    public void Attack(Vector2 point, float radius, float xDir)
    {
        if (canAttack)
        {
            canAttack = false;
            attacks[attackIndex].StartCircleAttack(point, radius, xDir);
            attackIndex = (attackIndex + 1) % attacks.Length;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }

}
