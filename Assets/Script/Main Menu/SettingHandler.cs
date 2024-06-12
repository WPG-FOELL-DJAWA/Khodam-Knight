using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;

public class SettingHandler : MonoBehaviour
{
    public static SettingHandler instance;
    [SerializeField] private AudioMixer _volumeMixer;
    [SerializeField] private GameObject _settingUIPanel;
    [SerializeField] private GenericButton _closeButton;
    [Header("Utilities")]
    [SerializeField] private int _maxResolutionOption;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Slider _BGMSlider;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private GenericButton _muteButton;
    [SerializeField] private GenericText _resolutionText;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 20;
    private Resolution selectedResolution;
    public string CurrentQualityName { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Application.targetFrameRate = 60;
        QualitySettings.SetQualityLevel(1);
        CurrentQualityName = QualitySettings.names[1];
    }
    void Start()
    {
        _BGMSlider.onValueChanged.AddListener(delegate { OnBGMChanged(_BGMSlider.value); });
        _SFXSlider.onValueChanged.AddListener(delegate { OnSFXChanged(_SFXSlider.value); });
        _resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(_resolutionDropdown.value); });

        _muteButton.BindButton(() =>
        {
            OnSFXChanged(0);
            _SFXSlider.value = 0;
        });

        // Screen.fullScreen = true;
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        _resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = resolutions.Length - _maxResolutionOption; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        selectedResolution = filteredResolutions[filteredResolutions.Count - 1];
        _resolutionText.SetText(selectedResolution.width.ToString() + "x" + selectedResolution.height.ToString());
    }

    private void OnEnable()
    {
        _closeButton.BindButton(() => closeSetting());
        settingShow();
    }

    private void settingShow()
    {
        _settingUIPanel.transform.localScale = Vector3.zero;
        _settingUIPanel.transform.DOScale(1, 2)
        .SetEase(Ease.Linear);
    }

    private void closeSetting()
    {
        _settingUIPanel.transform.DOScale(0, 2)
        .SetEase(Ease.Linear);
        _settingUIPanel.SetActive(false);
        MainMenuHandler.instance.closeSettingOrCredit();
    }

    private void SetResolution(int resolutionIndex)
    {
        selectedResolution = filteredResolutions[resolutionIndex];
        _resolutionText.SetText(selectedResolution.width.ToString() + "x" + selectedResolution.height.ToString());
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, true);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullScreen);
    }

    public void OnBGMChanged(float value)
    {
        if (value == 0)
            value = .1f;
        _volumeMixer.SetFloat("Mixer/BGM", Mathf.Log10(value) * 20);
    }

    public void OnSFXChanged(float value)
    {
        if (value == 0)
            value = .1f;
        _volumeMixer.SetFloat("Mixer/SFX", Mathf.Log10(value) * 20);
    }
}
