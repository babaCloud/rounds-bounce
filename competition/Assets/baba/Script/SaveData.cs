
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveData : MonoBehaviour
{

    private FileStream fileStream;//　ファイルストリーム
    private BinaryFormatter bf;//　バイナリフォーマッター
    private Data data;

    public HighScore highScore;

    void OnEnable()
    {
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    /// <summary>
    /// セーブしたい瞬間に呼ぶ
    /// </summary>
    public void Save()
    {
        bf = new BinaryFormatter();
        fileStream = null;

        try
        {
            //　ゲームフォルダにfiledata.datファイルを作成
            fileStream = File.Create(Application.dataPath + "/saveData.dat");

            //　クラスの作成
            data = new Data();

            //　クラスのデータに保存
            data.hiscore = highScore.hiScore.ToString();

            //　ファイルにクラスを保存
            bf.Serialize(fileStream, data);

        }
        catch (IOException e1)
        {
            Debug.Log("ファイルオープンエラー");
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }

    }

    /// <summary>
    /// データをロードしたい瞬間に呼ぶ
    /// </summary>
    public void Load()
    {
        bf = new BinaryFormatter();
        fileStream = null;

        try
        {
            //　ファイルを読み込む
            fileStream = File.Open(Application.dataPath + "/filedata.dat", FileMode.Open);

            //　読み込んだデータをデシリアライズ
            data = bf.Deserialize(fileStream) as Data;

            //ここで入れる
            highScore.hiScore = int.Parse(data.hiscore);

        }
        catch (FileNotFoundException e1)
        {
            Debug.Log("ファイルがありません");
        }
        catch (IOException e2)
        {
            Debug.Log("ファイルオープンエラー");
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }

    }


    [Serializable]
    class Data
    {
        public string hiscore;
    }

}
