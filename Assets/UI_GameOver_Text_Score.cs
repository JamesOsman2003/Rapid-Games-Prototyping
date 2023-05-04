using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_GameOver_Text_Score : MonoBehaviour
{
    
    public TextMeshProUGUI Text;
    public float score;

    private void Start()
    {
        Text.text = "Score : " + score;
    }

    public void ScoreUpdate(float scorePush)
    {
        score += scorePush;
        Text.text = "Score : " + score; 
    }
}
