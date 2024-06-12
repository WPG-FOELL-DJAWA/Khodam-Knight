
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputMainMenu : MonoBehaviour
{
    public static UserInputMainMenu instance;
    [SerializeField] private PlayerInput _playerInput;

    public bool anyKey;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _playerInput.SwitchCurrentActionMap("Main Menu");
    }

    public void OnAnyKey(InputValue value)
    {
        anyKey = value.isPressed;
    }
}
