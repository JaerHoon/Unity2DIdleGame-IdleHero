using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "AudioDate", menuName = "Scriptable Object/Audio Data", order = int.MaxValue)]

public class AudioStorage : ScriptableObject
{
    [SerializeField]
    SoundSrc[] soundSrc;
    public SoundSrc[] SoundSrc { get { return soundSrc; } }


}

[Serializable]
public struct SoundSrc
{
    [SerializeField]
    int id;
    [SerializeField]
    string name;
    [SerializeField]
    AudioClip audioClip;

    public AudioClip SoundFile { get { return audioClip; } }
    public int Id { get { return id; } }
}
