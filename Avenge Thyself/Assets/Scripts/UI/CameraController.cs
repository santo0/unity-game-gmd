using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;

    private void Start() 
    {
        offset = new Vector3(0,1f, (transform.position - target.position).z);

    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
    }
}