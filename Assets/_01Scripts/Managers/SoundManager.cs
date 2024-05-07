using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudioSource;
    public AudioMixer audioMixer;
    //public AudioMixer audioMixer_BGM;
    //public AudioMixer audioMixer_Effect;

    public Slider audioSlider_Master;
    public Slider audioSlider_BGM;
    public Slider audioSlider_Effect;

    [SerializeField]
    AudioStorage soundStorage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    public void AudioMasterControl()
    {
        float volume_Master = audioSlider_Master.value;
        if(volume_Master == -40f) audioMixer.SetFloat("Master", -80f);
        audioMixer.SetFloat("Master", volume_Master);
        
    }

    public void AudioBGMControl()
    {
        float volume_BGM = audioSlider_BGM.value;
        if (volume_BGM == -40f) audioMixer.SetFloat("BGM", -80f);
        audioMixer.SetFloat("BGM", volume_BGM);
    }

    public void AudioEffectControl()
    {
        float volume_Effect = audioSlider_Effect.value;
        if (volume_Effect == -40f) audioMixer.SetFloat("Effect", -80f);
        audioMixer.SetFloat("Effect", volume_Effect);
    }

    int SoundChangeNum = 1;

    public void OnChangeAudioClip()
    {
        myAudioSource.Stop();
        myAudioSource.clip = soundStorage.SoundSrc[SoundChangeNum % soundStorage.SoundSrc.Length].SoundFile;
        myAudioSource.Play();
        SoundChangeNum++;
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
