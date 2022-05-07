using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Vector2 spawnPosition;
    private void Awake()
    {
        spawnPosition = GetComponent<Transform>().position;
    }

    public void SpawnPlayer(GameObject player)
    {
        player.transform.position = spawnPosition;
        player.GetComponent<PlayerHealthSystem>().Revive();
    }

}
