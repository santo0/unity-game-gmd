using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public Slider slider;
    public void ChangeVolume()
    {
        if (0 <= slider.value && slider.value <= 1)
            AudioManager.instance.ChangeGeneralVolume(slider.value);
    }
}
