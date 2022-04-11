using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseReceiver : MonoBehaviour
{
    Transform target;
    public Transform GetTarget()
    {
        Debug.Log("GetTarget " + target);
        return target;
    }

    public void TargetPlayer(Transform player)
    {
        Debug.LogWarning("TargetPlayer " + player);
        target = player;
    }

    public void StopTarget()
    {
        Debug.LogWarning("StopTarget");
        target = null;
    }
}
