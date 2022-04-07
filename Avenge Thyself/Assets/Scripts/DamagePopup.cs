using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour, IPooledObject
{
    private TextMeshPro textMesh;

    static float lifeTime = 1;
    float outTime;
    static private float moveYSpeed = 5f;

    private void Awake()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
    }

    public void setDamage(float dmg)
    {
        textMesh.SetText(dmg + "!");
        Debug.Log("setDamage:" + outTime);
    }


    public void OnObjectSpawn()
    {
        textMesh.SetText("");
        outTime = lifeTime;
        Debug.Log("OnObjectSpawn:" + outTime);
    }


    private void FixedUpdate()
    {
            Debug.Log("FixedUpdate:" + outTime);
        if (outTime <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;
            outTime -= Time.deltaTime;
        }

    }



}
