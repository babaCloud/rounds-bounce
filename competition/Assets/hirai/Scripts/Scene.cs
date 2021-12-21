using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene
{
    private static Scene instance;
    public static Scene Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Scene();
            }
            return instance;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("ItemScene");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }
}