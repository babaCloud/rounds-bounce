using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Scene.Instance.LoadGameScene();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene.Instance.QuitGame();
        }
    }
}
