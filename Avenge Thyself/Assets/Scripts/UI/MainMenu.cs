using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;

    public void PlayGame()
    {
        GameManager.instance.LoadLevel("FirstLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
