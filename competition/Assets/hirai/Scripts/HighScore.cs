using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class HighScore : ScriptableObject
{
    public int hiScore;

    public int GetHighScore()
    {
        return hiScore;
    }

    public void SetHighScore(int newValue)
    {
        hiScore = newValue;
    }
}
