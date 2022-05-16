using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class PlayerStats
{

    public float criticProbability = 0.2f;
    public float healthPoints = 100;
    public float maxHealthPoints = 100;
    public float basicDamage = 10;

    //Always call this function for damage crit. effect
    public float GetTotalDamage()
    {
        float dmg = basicDamage;
        float n = Random.Range(0f, 1f);
        if (n <= criticProbability)
        {
            dmg *= 2;
        }
        return dmg;
    }

}
