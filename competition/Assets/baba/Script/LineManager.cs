using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private Vector3 mouseUp;
    private Vector3 mouseDown;

    public LineRenderer lineRender;
    public GameObject lineObj;
    public EdgeCollider2D[] edgeCollider;
    public GameObject[] edgeObj;

    public MainGameDebug mainData;
    private GameDirector gameD;
    void Start()
    {
        gameD = new GameDirector();
    }

    
    void Update()
    {
        if (!GameDirector.isGameMove) return;//時間が止まっている
        LineTransform();
        EdegeTransform();
    }

    /// <summary>
    /// 線を書く
    /// </summary>
    void LineTransform()
    {

        if (Input.GetMouseButtonDown(0))
        {           
            mouseDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDown.z = 0;
            mouseUp = mouseDown;
            lineRender.SetPosition(0, mouseDown);
            lineRender.SetPosition(1, mouseUp);

            lineObj.SetActive(true);
        }
        else if (Input.GetMouseButton(0))
        {
            mouseUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseUp.z = 0;
            lineRender.SetPosition(0, mouseDown);
            lineRender.SetPosition(1, mouseUp);
        }

    }

    /// <summary>
    /// 当たり判定をつける
    /// </summary>
    void EdegeTransform()
    {
        Vector2[] collPos = { new Vector2(0,0), new Vector2(0,0) };
        edgeObj[0].transform.position = new Vector3(lineRender.GetPosition(0).x, lineRender.GetPosition(0).y - 0.1f);
        edgeObj[1].transform.position = new Vector3(lineRender.GetPosition(0).x, lineRender.GetPosition(0).y + 0.1f);

        collPos[0].x = lineRender.GetPosition(1).x - edgeObj[0].transform.position.x - edgeCollider[0].offset.x;
        collPos[0].y = lineRender.GetPosition(1).y - edgeObj[0].transform.position.y - edgeCollider[0].offset.y;
        collPos[1].x = edgeCollider[0].offset.x * -1f;
        collPos[1].y = edgeCollider[0].offset.y * -1f;
        edgeCollider[0].points = collPos;
        edgeCollider[1].points = edgeCollider[0].points;
    }
}
