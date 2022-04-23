using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    override public void CharacterDeath()
    {
        Debug.LogWarning("Death of enemy animation");
        animator.SetTrigger("Death");
    }

    public override void CharacterRestart()
    {
        Destroy(gameObject);
    }
}
