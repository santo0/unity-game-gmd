using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, HealthSys
{


    public SpawnPoint spawnPoint;

    public Animator animator;

    public SpriteRenderer spriteRenderer;
    protected bool hittable;
    public float PUSH_TIME = 0.5f;
    public float DEAD_TIME = 2f;
    public PlayerStats playerStats;


    private void Awake()
    {
        hittable = true;

    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void Restart()
    {
        animator.SetTrigger("Alive");
        spawnPoint.SpawnPlayer(this.gameObject); //TODO: Set spawn
    }

    public void TakeHit(float damage, float xDir)
    {
        if (hittable)
        {
            StartCoroutine(TakeDamage_Cor(damage, xDir));
        }
    }

    IEnumerator TakeDamage_Cor(float damage, float xDir)
    {
        //Debug.LogWarning("antes "+ playerStats.healthPoints);
        hittable = false;
        playerStats.healthPoints -= damage;
        //Debug.LogWarning("despues "+ playerStats.healthPoints);
        DamagePopupSpawner.instance.SpawnDamagePopup(gameObject, damage);
        animator.SetTrigger("DamageTaken");
        spriteRenderer.material.SetInt("_Hit", 1);

        if (playerStats.healthPoints <= 0.0f)
        {
            spriteRenderer.material.SetInt("_Hit", 0);
            Death();
            yield return new WaitForSeconds(DEAD_TIME);
            Restart();
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir * 500f, 500f));
            yield return new WaitForSeconds(PUSH_TIME);
            spriteRenderer.material.SetInt("_Hit", 0);
            hittable = true;
        }
    }


    public bool IsHittable()
    {
        return hittable;
    }

    public void Revive()
    {
        playerStats.healthPoints = playerStats.maxHealthPoints;
        hittable = true;
        //Contador de vides o mort?
        GameManager.instance.BadGameOver();
    }
    public float GetHP()
    {
        return playerStats.healthPoints;
    }
}
