using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  = "MainGameDebugData")]
public class MainGameDebug : ScriptableObject
{
    [Header("書かれた線の位置")]
    public Vector2 lineStartPos;
    public Vector2 lineEndPos;

    [Header("玉の記録")]
    public Vector2 firstPos;
    public Vector2 secondPos;
}
