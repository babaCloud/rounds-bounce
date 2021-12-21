using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // お邪魔アイテム衝突時のフラグ
    public static bool obstacleFlg = false;
    //public bool ObstacleFlg { public get { return obstacleFlg; } set { obstacleFlg = value; } }

    public static Vector2 ObstanceVertexPos;

    // 回転のクォータニオン
    private Quaternion rot;
    // 回転する角度
    private float angle;
    // 回転する速度
    [SerializeField, Header("回転速度")]
    private float speed;
    [SerializeField, Header("頂点")]
    private GameObject vertexPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトを回転させる
        angle += speed;
        rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rot;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 弾に当たった場合非アクティブにし、弾の挙動が変わるフラグを渡す
        if (collider.tag == "Bullet")
        {
            Vector3 pos=this.gameObject.transform.TransformPoint(vertexPos.transform.position);
            ObstanceVertexPos = new Vector2(pos.x - this.gameObject.transform.position.x,
                pos.y - this.gameObject.transform.position.y);
            obstacleFlg = true;
            gameObject.SetActive(false);
        }
    }
}
