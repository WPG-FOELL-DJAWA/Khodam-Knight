// using StarterAssetsInput;
using UnityEngine;

public class CutSceneHandler : MonoBehaviour
{
    public static CutSceneHandler instance;
    [SerializeField] private bool isCutScene;
    // private ThirdPersonController _player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        // _player = FindAnyObjectByType<ThirdPersonController>();
    }
    public bool getCutSceneMode()
    {
        return isCutScene;
    }

    public void setCutSceneMode(bool cond)
    {
        isCutScene = cond;

        if (cond)
        {
            // _player.hideCharacterBody();
            CharacterInput.instance.changePlayerState(PlayerState.CutScene);
            // HUDHandler.instance.hideAllUI();
        }
        else
        {
            // _player.unHideCharacterBody();
            CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
            // HUDHandler.instance.unhideAllUI();

            TutorialHandler.instance.movementTutorial(); // noted : seharusnya tidak di sini
        }
    }

}
