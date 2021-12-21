using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{

    [SerializeField]
    private ScoreLogic scoreLogic;

    [SerializeField]
    private Text highScoreText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private HighScore highScoreObject;

    private int highScoreValue;
    private int scoreValue;

    private void OnEnable()
    {
        highScoreValue = highScoreObject.GetHighScore();
        scoreValue = scoreLogic.nowScore;

        if(highScoreValue < scoreValue)
        {
            highScoreValue = scoreValue;
        }

        scoreText.text = scoreValue.ToString();
        highScoreText.text = highScoreValue.ToString();

        highScoreObject.SetHighScore(highScoreValue);
    }

    public void OnClickRetry()
    {
        Scene.Instance.LoadGameScene();
    }

    public void OnClickTitle()
    {
        Scene.Instance.LoadTitleScene();
    }
}
