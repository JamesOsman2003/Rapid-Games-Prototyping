using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider; // Slider to adjust the amount of hea;th shown within border of health box

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // sets the slider Maximum value to be the maximum health 
    }

    public void SetHealth(float health)
    {
        slider.value = health; // sets the slider value to current value of health when called
    }
}
