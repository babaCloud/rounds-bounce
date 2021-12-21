using UnityEditor;
using UnityEngine;
public class SetHighScoreData : MonoBehaviour
{
    [SerializeField]
    ScriptableObject Table;
    void Start()
    {
        EditorUtility.SetDirty(Table);
        AssetDatabase.SaveAssets();
    }
}