using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelInformation[] levels;

    public bool StartInMenu;

    [SerializeField]
    private GameObject currentPlayer;
    public PlayerStats playerStats = null;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            playerStats = new PlayerStats();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //Debug option for loading menu or stay with current scene
        if (StartInMenu)
        {
            LoadLevel("Menu");
        }

        LevelInformation currentLevelInfo = Array.Find(levels,
                    level => level.levelName == SceneManager.GetActiveScene().name);
        
        AudioManager.instance.Play(currentLevelInfo.songName, true);

        currentPlayer = GameObject.Find("Player");

    }

    public void LoadLevel(string levelName)
    {
        Debug.Log(levelName);
        LevelInformation li = Array.Find(levels, level => level.levelName == levelName);
        StartCoroutine(Cor_LoadLevel(li));
    }


    IEnumerator Cor_LoadLevel(LevelInformation li)
    {
        //In case of transition, play it
        if (li.transition != null)
        {
            li.transition.animator.SetTrigger("Start");
            yield return new WaitForSeconds(li.transition.transitionTime);
            li.transition.animator.SetTrigger("End");
        }
        //Load level with its music
        SceneManager.LoadScene(li.levelNum);
        if (li.songName != null && li.songName != "")
        {
            AudioManager.instance.Play(li.songName, true);
        }
        else
        {
            AudioManager.instance.Stop();
        }

        //If loading level, restart player stats
        if (li.levelName == "Menu")
        {
            playerStats = new PlayerStats();
        }
        currentPlayer = GameObject.Find("Player");
    }


    public void GoodGameOver()
    {
        //Rick-roll 8)
        Debug.LogWarning("YOU WON!!!!!");
        LoadLevel("Credits");
    }
}
