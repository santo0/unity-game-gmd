using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    override public void CharacterDeath()
    {
        animator.SetTrigger("Death");
    }
}
