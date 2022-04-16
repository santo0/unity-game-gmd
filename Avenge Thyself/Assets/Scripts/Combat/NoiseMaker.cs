using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    CircleCollider2D noiseCircle;

    private void Awake()
    {
        noiseCircle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Ha entrat un enemic!");
            other.gameObject
                 .GetComponent<NoiseReceiver>()
                 .TargetPlayer(gameObject.transform.parent);
            //other.TargetPlayer
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Ha SORTIT un enemic!");
            other.gameObject
                 .GetComponent<NoiseReceiver>()
                 .StopTarget();
            //other.StopTargeting
        }
    }
}
