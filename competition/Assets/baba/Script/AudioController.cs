using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField, Header("音")]
    private AudioSource BGM;
    [SerializeField]
    private AudioSource SE;
    void Start()
    {
        BGM.pitch = 1.0f;
    }


    void Update()
    {
       
    }

    public void GameOverBGM()
    {
        if (BGM.pitch > 0.75f) BGM.pitch -= 0.01f;
    }

    public void SEPlay()
    {
        SE.Play();
    }
}
