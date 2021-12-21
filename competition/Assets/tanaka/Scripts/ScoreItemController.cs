using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 弾に当たった場合非アクティブにする
        if (collider.tag == "Bullet") gameObject.SetActive(false);
    }
}
