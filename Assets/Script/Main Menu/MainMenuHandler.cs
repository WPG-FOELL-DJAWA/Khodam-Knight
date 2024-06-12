//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: MainMenuHandler.cs
//  Description: Script to handle main menu
//
//  History:
//  - November 18, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler instance;
    [SerializeField] private BGMName _BGMName;

    [Header("Button")]
    [SerializeField] private List<GameObject> _allMainMenuButton;
    [SerializeField] private GenericButton _anyButton;
    [SerializeField] private GenericButton _playButton;
    [SerializeField] private GenericButton _settingButton;
    [SerializeField] private GenericButton _creditButton;
    [SerializeField] private GenericButton _QuitButton;
    [SerializeField] private GenericButton _newGameButton;
    [SerializeField] private GenericButton _LoadGameButton;
    [Header("Other Button")]
    [SerializeField] private GenericButton _backButton;

    [Header("Utilities")]
    [SerializeField] private SettingHandler _settingHandler;
    [SerializeField] private CreditHandler _creditHandler;
    [SerializeField] private LogoAnimation _logoAnimation;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _secondCamera;
    [SerializeField] private GameObject _thirdCamera;

    [Header("UI")]
    [SerializeField] private GameObject _logo;
    [SerializeField] private GameObject _mainMenuButton;
    [SerializeField] private GameObject _settingUI;
    [SerializeField] private GameObject _creditUI;
    [SerializeField] private GameObject _startGamePanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _anyButton.BindButton(() => startMenu());
        _playButton.BindButton(() => playGame());
        _settingButton.BindButton(() => openSetting());
        _creditButton.BindButton(() => openCredit());
        _QuitButton.BindButton(() => quitGame());
        _newGameButton.BindButton(() => playNewGame());
        _LoadGameButton.BindButton(() => playLoadGame());

        _backButton.BindButton(() => backButton());
    }

    private void Start()
    {
        AudioManager.instance.playBGM(_BGMName);
    }


    private IEnumerator entranceButton()
    {
        foreach (var item in _allMainMenuButton)
        {
            item.transform.localScale = Vector3.zero;
        }

        foreach (var item in _allMainMenuButton)
        {
            item.transform.DOScale(1, 1.5f)
            .SetEase(Ease.InOutBounce);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void startMenu()
    {
        _mainCamera.SetActive(false);
        _secondCamera.SetActive(true);

        _logoAnimation.menuMode();

        _anyButton.gameObject.SetActive(false);
        _mainMenuButton.SetActive(true);

        StartCoroutine(entranceButton());
    }

    private void openSetting()
    {
        _logo.SetActive(false);
        _mainMenuButton.SetActive(false);
        _secondCamera.SetActive(false);
        _thirdCamera.SetActive(true);

        _settingUI.SetActive(true);
    }

    private void openCredit()
    {
        _logo.SetActive(false);
        _mainMenuButton.SetActive(false);
        _secondCamera.SetActive(false);
        _thirdCamera.SetActive(true);

        _creditUI.SetActive(true);
    }

    private void playGame()
    {
        _startGamePanel.SetActive(true);
        _mainMenuButton.SetActive(false);
        _logo.SetActive(false);
    }

    private void backButton()
    {
        _logo.SetActive(true);
        _mainMenuButton.SetActive(true);
        StartCoroutine(entranceButton());
        _startGamePanel.SetActive(false);

    }

    private void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void playNewGame()
    {
        LoadScene.instance.loadScene(_playerSO.lastScene);
    }

    private void playLoadGame()
    {

    }




    public void closeSettingOrCredit()
    {
        _thirdCamera.SetActive(false);
        _secondCamera.SetActive(true);
        _logo.SetActive(true);
        _mainMenuButton.SetActive(true);
        StartCoroutine(entranceButton());
    }
}
