using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLogic : MonoBehaviour
{

    [SerializeField,Header("アイテム一個取ったときのポイント")]
    private int pointValue;

    [HideInInspector]
    public int nowScore;

    [SerializeField,Header("スコア表示のテキスト")]
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (pointValue == 0)
            pointValue = 100;

        nowScore = 0;
    }

    /// <summary>
    /// スコアアイテムに当たったときに呼べ(コンボだけ入れて)
    /// </summary>
    /// <param name="combo"></param>
    public void GetPoint(int combo)
    {
        nowScore += combo * pointValue;
        scoreText.text = nowScore.ToString();
    }


}
