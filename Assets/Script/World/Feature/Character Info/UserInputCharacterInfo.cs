using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputCharacterInfo : MonoBehaviour
{
    public static UserInputCharacterInfo instance;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerSO _playerSO;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        _playerInput.SwitchCurrentActionMap("Character Info");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnCloseCharacterInfo(InputValue value)
    {
        LoadScene.instance.loadScene(_playerSO.lastScene);
    }
}
