using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound instance;

    private void Awake()
    {
        if (PlayerSound.instance == null)
            PlayerSound.instance = this;
    }

    [SerializeField]
    AudioStorage audiostor;
    AudioSource myaudio;
    
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
    }

    public void OnwpaponSound()
    {
        myaudio.PlayOneShot(audiostor.SoundSrc[0].SoundFile);
    }

    public void OnEarthSound()
    {
        myaudio.PlayOneShot(audiostor.SoundSrc[1].SoundFile);
    }

    public void OnTornadoSound()
    {
        myaudio.PlayOneShot(audiostor.SoundSrc[2].SoundFile);
        StartCoroutine(stopSound());
    }

    IEnumerator stopSound()
    {
        yield return new WaitForSeconds(1.0f);
        myaudio.Stop();
    }

    public void OnWindSound()
    {
        myaudio.PlayOneShot(audiostor.SoundSrc[3].SoundFile);
    }

    public void OnMeteorSound()
    {
        myaudio.PlayOneShot(audiostor.SoundSrc[4].SoundFile);
        StartCoroutine(playExSound());
    }

    IEnumerator playExSound()
    {
        yield return new WaitForSeconds(1.8f);
        myaudio.Stop();
        myaudio.PlayOneShot(audiostor.SoundSrc[5].SoundFile);
    }

    public void OnBuffSound()
    {
        myaudio.PlayOneShot(audiostor.SoundSrc[6].SoundFile);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
