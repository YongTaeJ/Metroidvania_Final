using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : SoundManager<SFX>
{
    #region Set SFX

    public AudioSource AudioSource { get; set; }
    private float volumeScale = 1.0f;
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

    protected override void SetVolume(float volumeScale)
    {
        SetVolume("SFX", volumeScale);
    }

    #endregion

    // 예시
    [Header("UI")]
    public AudioClip btnClick;

    [Header("Item")]
    public AudioClip itemDrop;

    [Header("Player")]
    public AudioClip playerAttack;

    [Header("Enemy")]
    public AudioClip enemyAttack;


    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
    {
        this.AudioSource.PlayOneShot(clip, volumeScale);
    }
}
