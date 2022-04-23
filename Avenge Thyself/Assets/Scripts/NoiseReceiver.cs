using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseReceiver : MonoBehaviour
{
    Transform target;
    public Transform GetTarget()
    {
        return target;
    }

    public void TargetPlayer(Transform player)
    {
        target = player;
    }

    public void StopTarget()
    {
        target = null;
    }
}
