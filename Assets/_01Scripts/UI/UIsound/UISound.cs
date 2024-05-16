using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public static UISound instance; 

    [SerializeField]
    AudioStorage audios;

    public const int pressButton = 2;
    public const int pressStatUp = 3;
    public const int pressEquipItem = 4;
    public const int pressEquipSkill = 5;
    public const int pressQuestReward = 6;
    public const int pressSummons = 8;

    AudioSource myAudio;

    private void Awake()
    {
        if (instance == null) instance = this;

        myAudio = GetComponent<AudioSource>();
    }


    public void PlayerSound(int num)
    {
        myAudio.PlayOneShot(audios.SoundSrc[num].SoundFile);
    }
}
