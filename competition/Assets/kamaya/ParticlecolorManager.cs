using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�X�R�A�A�C�e���̐F��col�ɓn�����Ƃ��̐F�̃p�[�e�B�N���ɂȂ�

public class ParticlecolorManager : MonoBehaviour
{
    [SerializeField] public Color col;   //�n���F
    [SerializeField] ParticleSystem[] particles = new ParticleSystem[3];   //�q�I�u�W�F�N�g�̃p�[�e�B�N��
    ParticleSystem.MainModule[] particleMainMods = new ParticleSystem.MainModule[3];   //�p�[�e�B�N���̃��C�����W���[��

    //void Start()
    //{
    //    ���ׂẴp�[�e�B�N�����烁�C�����W���[�����擾���ĐF��ς���
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
