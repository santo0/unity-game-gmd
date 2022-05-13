using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulBody : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool isBlocking = other.gameObject.GetComponent<PlayerCombat>().IsBlocking();
            if (isBlocking)
            {
                Debug.Log("BLOQUEJAT");
            }
            else
            {
                Debug.Log("Hostion");
                PlayerHealthSystem playerHS = other.gameObject.GetComponent<PlayerHealthSystem>();
                playerHS.TakeHit(damage, 1); //TODO: Canviar aixo!
            }
        }
    }

}
