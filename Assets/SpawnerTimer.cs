using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerTimer : MonoBehaviour
{
    public Slider slider; // Slider to adjust the amount of heath shown within border of health box

    public void SetMaxNextWaveTime(float TimeBeforeNextWave)
    {
        slider.maxValue = TimeBeforeNextWave; // sets the slider Maximum value to be the maximum health 
    }

    public void SetWaveTime(float TimeBeforeNextWave)
    {
        slider.value = TimeBeforeNextWave; // sets the slider value to current value of health when called
    }
}
