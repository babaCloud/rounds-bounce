using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static bool isGameMove;

    private void Awake()
    {
        GameStart(false);
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           GameStart(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene.Instance.QuitGame();
        }
    }

    /// <summary>
    /// ゲーム内の時間管理
    /// </summary>
    /// <param name="_isStart"></param>
    /// <returns></returns>
    public void GameStart(bool _isStart)
    {
        isGameMove = _isStart;
    }
}
