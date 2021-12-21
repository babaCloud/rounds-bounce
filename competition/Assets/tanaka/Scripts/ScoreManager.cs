using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    // スコアアイテムのコンボ数
    private int combo;

    [SerializeField]
    private ScoreLogic scoreLogic;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // スコアアイテム以外に当たった場合コンボをリセットする
        if (collision.gameObject.tag != "Score")
        {
            combo = 0;
            return;
        }
        // コンボを加算する
        combo++;
        scoreLogic.GetPoint(combo);
    }
}
