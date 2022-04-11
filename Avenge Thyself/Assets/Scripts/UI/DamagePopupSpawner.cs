using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void SpawnDamagePopup(GameObject target, float dmg)
    {

        DamagePopup dmgPu = objectPooler
                            .SpawnFromPool("dmgPopup", target.transform.position)
                            .GetComponent<DamagePopup>();
        dmgPu.OnObjectSpawn();
        dmgPu.setDamage(dmg);
    }
}
