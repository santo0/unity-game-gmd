using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{

    public SpawnPoint spawnPoint;

    override public void CharacterDeath()
    {
        animator.SetBool("Dead", true);
    }

    public override void CharacterRestart()
    {
        animator.SetBool("Dead", false);
        spawnPoint.SpawnPlayer(this.gameObject);

    }

    public void Revive()
    {
        hp = MAX_HP;
    }
}
