using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSys : EnemyHealthSys
{

    public override void Restart()
    {
        GameManager.instance.GoodGameOver();
        Destroy(gameObject);
    }


}
