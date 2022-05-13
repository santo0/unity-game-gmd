using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI currentHP;
    public TextMeshProUGUI maxHP;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        currentHP.text = health.ToString();
        maxHP.text = health.ToString();

    }

    public void SetHealth(float health)
    {
        slider.value = health;
        currentHP.text = health.ToString();

    }

}
