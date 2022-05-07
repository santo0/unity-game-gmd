using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthSys
{
    public void Death();
    public void Restart();

    public void TakeHit(float damage, float xDir);

    public bool IsHittable();

    public float GetHP();

}
