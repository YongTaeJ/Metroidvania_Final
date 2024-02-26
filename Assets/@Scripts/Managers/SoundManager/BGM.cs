using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BGM : SoundManager<BGM>
{
    #region Set BGM

    public AudioSource AudioSource { get; set; }
    private float volumeScale = 0.2f;
    public float VolumeScale
    {
        get
        {
            return this.volumeScale;
        }
        set
        {
            this.volumeScale = Mathf.Clamp01(value);
            SetVolume(this.volumeScale);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        this.AudioSource = GetComponent<AudioSource>();
        LoadVolumeSettings();
    }

    protected override void SetVolume(float volumeScale)
    {
        SetVolume("BGM", volumeScale);
        Debug.Log("볼륨 조절");
    }

    #endregion

    private void Start()
    {
        Play(ResourceManager.Instance.GetAudioClip("StartScene"), true); //시작 브금 스타트
        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume",0.5f);
        VolumeScale = savedVolume;
    }

    public void Play(AudioClip clip, bool isLoop)
    {
        this.AudioSource.loop = isLoop;
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }

    public void Play(string name, bool isLoop)
    {
        AudioClip clip = ResourceManager.Instance.GetAudioClip(name);
        this.AudioSource.loop = isLoop;
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }

    public void Stop()
    {
        this.AudioSource.Stop();
    }
}
