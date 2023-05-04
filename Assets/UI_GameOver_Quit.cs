using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UI_GameOver_Quit : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadSceneAsync(0);
        SceneManager.UnloadSceneAsync(1);
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
    }
}
