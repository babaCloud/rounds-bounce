using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スコアアイテムの色がcolに渡されるとその色のパーティクルになる

public class ParticlecolorManager : MonoBehaviour
{
    [SerializeField] public Color col;   //渡す色
    [SerializeField] ParticleSystem[] particles = new ParticleSystem[3];   //子オブジェクトのパーティクル
    ParticleSystem.MainModule[] particleMainMods = new ParticleSystem.MainModule[3];   //パーティクルのメインモジュール

    //void Start()
    //{
    //    すべてのパーティクルからメインモジュールを取得して色を変える
    //    for (int i = 0; i < particles.Length; i++)
    //    {
    //        particleMainMods[i] = particles[i].main;
    //        particleMainMods[i].startColor = col;
    //    }
    //}

    public void StartEffect()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particleMainMods[i] = particles[i].main;
            particleMainMods[i].startColor = col;
        }
    }




}
