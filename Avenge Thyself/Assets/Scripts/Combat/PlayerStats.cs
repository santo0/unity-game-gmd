using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Player Stats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableSingleton<PlayerStats>
{
    public float criticProbability;
    public float healthPoints;
    public float maxHealthPoints;
    public float basicDamage;

    public float stamina;


    public float GetTotalDamage()
    {
        float dmg = basicDamage;
        float n = Random.Range(0f, 1f); //TODO: ARREGLAR AIXO! SEMPRE FA CRITICS
        if (n <= criticProbability)
        {
            dmg *= 2;
        }
        return dmg;
    }

}
