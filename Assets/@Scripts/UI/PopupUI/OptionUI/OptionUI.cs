using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    #region Field

    [SerializeField] private GameObject _sound;
    [SerializeField] private GameObject _graphic;

    [Header("Buttons")]
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button graphicBtn;
    [SerializeField] private Button backBtn;

    [Header("Slider")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("InputField")]
    [SerializeField] private TMP_InputField bgmInputField;
    [SerializeField] private TMP_InputField sfxInputField;

    #endregion

    #region Unity Flow

    private void Start()
    {
        soundBtn.onClick.AddListener(OnSoundMaster);
        graphicBtn.onClick.AddListener(OnGraphicMaster);
        backBtn.onClick.AddListener(OnPause);

        bgmSlider.onValueChanged.AddListener(OnBgmVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChange);

        bgmInputField.onEndEdit.AddListener(OnBgmVolumeEdit);
        sfxInputField.onEndEdit.AddListener(OnSfxVolumeEdit);

        Initialize();
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Initialize()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume",0.5f);
        BGM.Instance.VolumeScale = bgmVolume;
        bgmSlider.value = bgmVolume;
        bgmInputField.text = Mathf.RoundToInt(bgmVolume * 100).ToString();

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume",0.5f);
        SFX.Instance.VolumeScale = sfxVolume;
        sfxSlider.value = sfxVolume;
        sfxInputField.text = Mathf.RoundToInt(sfxVolume * 100).ToString();
    }

    #endregion

    #region OnClick

    private void OnSoundMaster()
    {
        _graphic.SetActive(false);
        _sound.SetActive(true);
    }

    private void OnGraphicMaster()
    {
        _sound.SetActive(false);
        _graphic.SetActive(true);
    }

    private void OnPause()
    {
        UIManager.Instance.ClosePopupUI(PopupType.Option);
        UIManager.Instance.OpenPopupUI(PopupType.Pause);
    }

    #endregion

    #region OnValueChanged

    private void OnBgmVolumeChange(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
        int intValue = Mathf.RoundToInt(value * 100);
        BGM.Instance.VolumeScale = value;
        bgmInputField.text = intValue.ToString();
    }

    private void OnSfxVolumeChange(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
        int intValue = Mathf.RoundToInt(value * 100);
        SFX.Instance.VolumeScale = value;
        sfxInputField.text = intValue.ToString();
    }


    #endregion

    #region OnEndEdit

    private void OnBgmVolumeEdit(string value)
    {
        ProcessVolumeEdit(value, bgmSlider, true);
    }

    private void OnSfxVolumeEdit(string value)
    {
        ProcessVolumeEdit(value, sfxSlider, false);
    }

    private void ProcessVolumeEdit(string value, Slider targetSlider, bool isBgm)
    {
        if (int.TryParse(value, out int intValue))
        {
            intValue = Mathf.Clamp(intValue, 0, 100);
            float floatValue = intValue / 100f; // 0에서 100 범위를 0.0에서 1.0으로 변환

            targetSlider.value = floatValue; // 슬라이더 업데이트

            if (isBgm)
            {
                BGM.Instance.VolumeScale = floatValue; // BGM 볼륨 업데이트
            }
            else
            {
                SFX.Instance.VolumeScale = floatValue; // SFX 볼륨 업데이트
            }
        }
        else
        {
            // 유효하지 않은 입력 처리: 슬라이더의 현재 값으로 되돌림
            targetSlider.value = isBgm ? BGM.Instance.VolumeScale : SFX.Instance.VolumeScale;
        }
    }

    #endregion
}