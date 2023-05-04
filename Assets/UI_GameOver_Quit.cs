using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UI_GameOver_Quit : MonoBehaviour
{
    public void Quit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit(0);
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
    }
}
