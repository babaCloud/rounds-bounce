using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField]
    private GameObject onGameScoreCanvas;

    public float speed;
    public string lineTagName;
    public string scoreTagName;
    public LineRenderer line;
    public LineRenderer normalLine;
    public GameObject normalVertexObj;
    public GameObject gameOverCanvas;
    public AudioController audio;
    public GameObject effectObj;
    public ParticleSystem[] effect;

    public MainGameDebug mainData;
    private GameDirector gameD;
    private ObstacleController obs;
    private ParticlecolorManager parMan;
    private bool isGameOver = false;

    void Start()
    {
        mainData.secondPos = this.gameObject.transform.position;
        mainData.firstPos = this.gameObject.transform.position;
        gameD = new GameDirector();
        obs = new ObstacleController();
        gameOverCanvas.SetActive(false);
        onGameScoreCanvas.SetActive(true);
        isGameOver = false;
        parMan = effectObj.GetComponent<ParticlecolorManager>();
    }

    
    void FixedUpdate()
    {
        if (isGameOver) audio.GameOverBGM();
        if (!GameDirector.isGameMove) return;//時間が止まっている
        BallMove();
        if (ObstacleController.obstacleFlg) BallItemReflection();
        
    }

    void BallMove()
    {
        this.gameObject.transform.position += new Vector3(transform.right.x * speed, transform.right.y * speed, 0);
    }


    void OnBecameInvisible()
    {
        isGameOver = true;
        gameD.GameStart(false);
        gameOverCanvas.SetActive(true);
        onGameScoreCanvas.SetActive(false);

    }

    /// <summary>
    /// 正常反射
    /// </summary>
   void BallReflection()
    {
        mainData.firstPos = mainData.secondPos;
        mainData.secondPos = this.gameObject.transform.position;

        //元のベクトル
        Vector2 ballVec = mainData.secondPos - mainData.firstPos;
        Vector2 lineVec = line.GetPosition(1) - line.GetPosition(0);
        float lineVecAngle = Vector3.Angle(lineVec, new Vector3(1, 0, 0));

        //書いた線の法線ベクトル(逆ベクトルも含む)
        //normalLine.gameObject.transform.position = this.gameObject.transform.position;       
        //normalLine.gameObject.transform.localEulerAngles = new Vector3(0, 0, lineVecAngle + 90);
        //Vector2 normalVertex = line.transform.TransformPoint(normalVertexObj.transform.position); 
        //Vector2 normalVec = new Vector2(normalVertex.x - this.gameObject.transform.position.x,
        //    normalVertex.y - this.gameObject.transform.position.y);

    
        //計算　消す　動く       
        line.gameObject.SetActive(false);
        //transform.RotateAround(this.gameObject.transform.position, normalVec, 180.0f);
        //transform.eulerAngles = new Vector3(0f, 0f, -this.gameObject.transform.eulerAngles.z);
        transform.RotateAround(this.gameObject.transform.position, lineVec, 180.0f);
    }

    /// <summary>
    /// おじゃまアイテムにあたった時の反射
    /// </summary>
    void BallItemReflection()
    {
        ObstacleController.obstacleFlg = false;
       
        var dir =Quaternion.FromToRotation(new Vector3(1,0,0), ObstacleController.ObstanceVertexPos);
        this.gameObject.transform.rotation = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == lineTagName)
        {
            BallReflection();
        }

        if (collision.gameObject.tag == scoreTagName)
        {
            effectObj.transform.position = this.gameObject.transform.position;
            for(int i = 0; i < effect.Length; i++)
            {
                parMan.col = collision.GetComponent<SpriteRenderer>().color;
                parMan.StartEffect();
                effect[i].Play();
            }
            audio.SEPlay();
        }
    }
}
