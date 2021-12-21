using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemGenerator : MonoBehaviour
{
    // 生成するスコアアイテム
    private GameObject item;
    // 生成したアイテムのリスト
    List<GameObject> itemLists = new List<GameObject>();

    // スクリーン内の原点
    private Vector3 origin;
    // 生成範囲の大きさ(最大値・最小値の絶対値)
    private float geneAbsX, geneAbsY;
    // スクリーンの全体から生成する範囲の割合
    [SerializeField,Header("スクリーンの大きさに対しての生成範囲の割合")]
    private float percent;

    // 最大生成数
    private const int GENERATE_MAX = 8;
    // 始めの生成間隔
    [SerializeField,Header("始めの生成間隔(秒)")]
    private float firstSpan = 1.0f;
    // 生成間隔の上昇速度
    [SerializeField,Header("生成間隔の上昇速度(秒)")]
    private float geneSpeed = 0.2f;

    // アイテムのレンダラー
    private List<SpriteRenderer> itemRenderer = new List<SpriteRenderer>();
    // レンダラーで変更する色
    private Color[] scoreColors = new Color[]
    {
        new Color32(148, 255, 247, 255),
        new Color32(159, 255, 148, 255),
        new Color32(240, 255, 148, 255),
        new Color32(255, 211, 148, 255),
        new Color32(255, 155, 148, 255),
        new Color32(255, 148, 255, 255),
        new Color32(239, 148, 255, 255),
        new Color32(148, 159, 255, 255)
    };

    /// <summary>
    /// カメラのサイズから生成範囲を計算する
    /// </summary>
    public float InputGenerateRange(Vector3 origin, float per, string xy)
    {
        // スクリーンの幅・高さと原点を取得する
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 cameraPos = new Vector3();
        cameraPos = mainCamera.ScreenToWorldPoint(cameraPos);
        float scrWidth = cameraPos.x;
        float scrHeight = cameraPos.y;
        origin = Vector3.zero;

        // 生成範囲の大きさを計算し、引数によって範囲の幅・高さを取得する
        if (xy == "x") return Mathf.Lerp(origin.x, scrWidth, per);
        if (xy == "y") return Mathf.Lerp(origin.y, scrHeight, per);

        return 0;
    }

    /// <summary>
    /// スタート時にアイテムを最大生成数分生成しておく
    /// </summary>
    /// <param name="geneItems"></param>
    /// <param name="item"></param>
    /// <param name="renderer"></param>
    public void GenerateItemAgo(List<GameObject> geneItems, GameObject item, int MAX)
    {
        for (int i = 0; i < MAX; ++i)
        {
            // アイテムをあらかじめ生成し、リストに追加する
            geneItems.Add(Instantiate(item));
            // 非アクティブにしておく
            geneItems[i].SetActive(false);

            // アイテムのレンダラーをリストに追加する(スコアアイテムのみ)
            if (item.name == "ScoreItem") itemRenderer.Add(item.GetComponent<SpriteRenderer>());
        }
    }

    /// <summary>
    /// 生成範囲を可視化する(デバッグ用)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="origin"></param>
    public void ShowGenerateRange(float x, float y, Vector3 origin, Color color)
    {
        // 範囲の四隅に置く点のオブジェクトを取得する
        GameObject rangePoint = (GameObject)Resources.Load("RangePoint");

        // 点オブジェクトの色を変更する
        SpriteRenderer pointSprite = rangePoint.GetComponent<SpriteRenderer>();
        pointSprite.color = color;

        // 範囲の四隅の座標を取得する
        Vector3 rightUp = new Vector3(x, y, origin.z);
        Vector3 rightDown = new Vector3(x, -y, origin.z);
        Vector3 leftUp = new Vector3(-x, y, origin.z);
        Vector3 leftDown = new Vector3(-x, -y, origin.z);

        // 取得した座標に点オブジェクトを生成する
        Instantiate(rangePoint, leftUp, Quaternion.identity);
        Instantiate(rangePoint, rightUp, Quaternion.identity);
        Instantiate(rangePoint, leftDown, Quaternion.identity);
        Instantiate(rangePoint, rightDown, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 生成範囲を計算する
        geneAbsX = InputGenerateRange(origin, percent, "x");
        geneAbsY = InputGenerateRange(origin, percent, "y");

        // 生成範囲の四隅に点を置いて範囲を可視化させる
        //ShowGenerateRange(geneAbsX, geneAbsY, origin, Color.blue);

        // 生成アイテムのプレハブを取得
        item = (GameObject)Resources.Load("ScoreItem");

        // あらかじめアイテムを生成しておく
        GenerateItemAgo(itemLists, item, GENERATE_MAX);

        // 生成コルーチンを開始する
        StartCoroutine(GenerateItem(firstSpan, geneSpeed, GENERATE_MAX, geneAbsX, geneAbsY, itemLists));
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// アイテムを生成する
    /// </summary>
    /// <param name="span"></param>
    /// <returns></returns>
    public IEnumerator GenerateItem(float span, float speed, int COUNT_MAX, float absX, float absY,List<GameObject> geneItems)
    {
        // 生成する座標値
        float posX = 0f, posY = 0f;
        // スコアアイテムの変更色の要素番号
        int colNum = 0;
        // 生成のカウント
        int geneCount = 0;

        // 生成間隔が0になるまで生成し続ける(無限ループ)
        while (true)
        {
            // 0になる場合生成速度を最大に固定する
            if (span <= 0) span = speed;

            // 生成(コルーチン)停止
            yield return new WaitForSeconds(span);

            // 最大生成数まで生成する
            if (COUNT_MAX <= geneCount)
            {
                // 生成のカウントをリセットする
                geneCount = 0;
                // 生成間隔を早める
                span -= speed;
            }
            else
            {
                // 前に生成したアイテムの座標を取得する
                float beforeX = posX;
                float beforeY = posY;
                // ランダムで生成する座標値を決める
                posX = Random.Range(absX, -geneAbsX);
                posY = Random.Range(absY, -geneAbsY);
                // 前に生成したアイテムと座標が重複した場合決め直す
                if (beforeX == posX)
                    while(beforeX == posX) posX = Random.Range(absX, -geneAbsX);
                else if (beforeY == posY)
                    while(beforeY == posY) posY = Random.Range(absY, -geneAbsY);
                // 範囲内にランダムでアイテムを生成する
                Vector3 itemPos = new Vector3(posX, posY, origin.z);

                if (geneItems[geneCount].activeSelf == false)
                {
                    // スコアアイテムのみ色を変更する
                    if (geneItems[geneCount].name == "ScoreItem(Clone)")
                    {
                        // アイテムのレンダラーを取得する
                        itemRenderer[geneCount] = itemLists[geneCount].GetComponent<SpriteRenderer>();
                        // 取得するカラーの要素番号を繰り返す
                        colNum = colNum < scoreColors.Length && colNum > 0 ? colNum : 0;
                        // 画像の色を変更する
                        itemRenderer[geneCount].color = scoreColors[colNum];
                        colNum++;
                    }

                    // オブジェクトのポジションを変更し、アクティブにする
                    geneItems[geneCount].transform.position = itemPos;
                    geneItems[geneCount].SetActive(true);
                }

                geneCount++;

            }
        }
    }
}
