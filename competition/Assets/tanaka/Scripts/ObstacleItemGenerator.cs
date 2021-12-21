using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleItemGenerator : MonoBehaviour
{
    // 生成するお邪魔アイテム
    private GameObject item;
    // 生成したアイテムのリスト
    List<GameObject> geneItems = new List<GameObject>();

    // スクリーン内の原点
    private Vector3 origin;
    // 生成範囲の大きさ(最大値・最小値の絶対値)
    private float geneAbsX, geneAbsY;
    // スクリーンの全体から生成する範囲の割合
    [SerializeField, Header("スクリーンの大きさに対しての生成範囲の割合")]
    private float percent;

    // 最大生成数
    private const int GENERATE_MAX = 1;
    // 始めの生成間隔
    [SerializeField, Header("始めの生成間隔(秒)")]
    private float firstSpan = 1.0f;
    // 生成間隔の上昇速度
    [SerializeField, Header("生成間隔の上昇速度(秒)")]
    private float geneSpeed = 0.2f;

    // スコアアイテム生成スクリプト
    private ScoreItemGenerator generator;

    // Start is called before the first frame update
    void Start()
    {
        // スコアアイテム生成スクリプトを取得する
        generator = GameObject.Find("ScoreItemGenerator").GetComponent<ScoreItemGenerator>();

        // 生成範囲を計算する
        geneAbsX = generator.InputGenerateRange(origin, percent, "x");
        geneAbsY = generator.InputGenerateRange(origin, percent, "y");

        // 生成範囲の四隅に点を置いて範囲を可視化させる
        //generator.ShowGenerateRange(geneAbsX, geneAbsY, origin, Color.red);

        // 生成アイテムのプレハブを取得
        item = (GameObject)Resources.Load("ObstacleItem");

        // あらかじめアイテムを生成しておく
        generator.GenerateItemAgo(geneItems, item, GENERATE_MAX);

        // 生成コルーチンを開始する
        StartCoroutine(generator.GenerateItem(firstSpan, geneSpeed, GENERATE_MAX, geneAbsX, geneAbsY, geneItems));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
