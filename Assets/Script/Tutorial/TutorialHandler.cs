
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public static TutorialHandler instance;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private GameObject _movementTutorialUI;
    private GameObject _currentTutorial;
    private bool _isStartTutorial = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void movementTutorial()
    {
        if (!_playerSO.tutorialData.movementTutorialDone)
        {
            _currentTutorial = _movementTutorialUI;
            _isStartTutorial = true;

            CharacterInput.instance.changePlayerState(PlayerState.Tutorial); // noted : sementara langsung di sini
            CharacterInput.instance.changeMoveAndLookToZero(); // noted : sementara di disabe sampai input baru jadi
            Time.timeScale = 0;
            _currentTutorial.SetActive(true);
            HUDHandler.instance.hideAllUIGroup();
        }
    }

    public void closeCurrentTutorial()
    {
        if (_currentTutorial != null)
        {
            _currentTutorial.SetActive(false);
            _isStartTutorial = false;

            _playerSO.tutorialData.movementTutorialDone = true;
            CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
            Time.timeScale = 1f;
            HUDHandler.instance.showAllUIGroup();
        }
    }

    public bool getIsStartTutorial()
    {
        return _isStartTutorial;
    }

}
