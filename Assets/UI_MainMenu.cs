using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{
    public void Begin()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.UnloadSceneAsync(0);
    }

    public void Controls()
    {
        SceneManager.LoadSceneAsync(2);
        SceneManager.UnloadSceneAsync(0);
    }

    public void ControlsReturn()
    {
        SceneManager.LoadSceneAsync(0);
        SceneManager.UnloadSceneAsync(2);
    }

    public void Quit()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit(0);
    }
}
