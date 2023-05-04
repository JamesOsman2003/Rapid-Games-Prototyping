using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar_UI : MonoBehaviour
{
    public Slider slider; // Slider to adjust the amount of heath shown within border of health box

    public void SetMaxHealth(float MaxHealth)
    {
        slider.maxValue = MaxHealth; // sets the slider Maximum value to be the maximum health 
    }

    public void SetHealth(float Health)
    {
        slider.value = Health; // sets the slider value to current value of health when called
    }
}
