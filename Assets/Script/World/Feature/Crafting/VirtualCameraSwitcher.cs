//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: VirtualCameraSwitcher.cs
//  Description: Script for switch chinemachine camera in world view
//
//  History:
//  - November 7, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;

public class VirtualCameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _targetCamera;
    [SerializeField] private bool isHidePlayer;
    private GameObject _mainCamera;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _mainCamera = GameObject.FindGameObjectWithTag("MainVirtualCamera");
    }

    public void roamingMode()
    {
        if (isHidePlayer)
            _player.SetActive(true);

        _mainCamera.SetActive(true);
        _targetCamera.SetActive(false);
    }

    public void virtualMode()
    {
        if (isHidePlayer)
            _player.SetActive(false);

        _mainCamera.SetActive(false);
        _targetCamera.SetActive(true);
    }
}
