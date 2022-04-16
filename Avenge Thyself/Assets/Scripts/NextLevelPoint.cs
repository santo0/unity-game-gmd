using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelPoint : MonoBehaviour
{
    public LevelLoader levelLoader;


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollision");
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("It's a player");
            levelLoader.LoadNextLevel();
        }
    }
}
