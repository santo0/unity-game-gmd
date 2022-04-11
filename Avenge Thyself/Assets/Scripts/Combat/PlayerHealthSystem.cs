using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{

    override public void CharacterDeath()
    {
        animator.SetTrigger("Death");
    }
}
