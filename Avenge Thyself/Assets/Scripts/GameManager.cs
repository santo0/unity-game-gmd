using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelInformation[] levels;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //Load menu
        LoadLevel("Menu");
    }


    public void LoadLevel(string levelName)
    {
        LevelInformation li = Array.Find(levels, level => level.levelName == levelName);
        StartCoroutine(Cor_LoadLevel(li));
    }


    IEnumerator Cor_LoadLevel(LevelInformation li)
    {
        Debug.Log("Cor_LoadLevel: " + li.levelName);
        if(li.transition != null){
            li.transition.animator.SetTrigger("Start");
            yield return new WaitForSeconds(li.transition.transitionTime);
            li.transition.animator.SetTrigger("End");
        }
        SceneManager.LoadScene(li.levelNum);
        AudioManager.instance.Play(li.songName, true);
        
    }

}
