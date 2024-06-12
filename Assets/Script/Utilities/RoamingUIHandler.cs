using UnityEngine;

public class RoamingUIHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _interactUI;

    [Header("Button")]
    [SerializeField] private GenericButton _openMiniMapButton;
    [SerializeField] private GenericButton _openQuest;

    [Space]
    [SerializeField] private GameObject _nonEssentialRoamingUI;

    private void Start()
    {
        _openMiniMapButton.BindButton(() => HUDHandler.instance.openMiniMap());
        _openQuest.BindButton(() => HUDHandler.instance.openQuest());
    }


    public void showInteractUI() => _interactUI.SetActive(true);
    public void hideInteractUI() => _interactUI.SetActive(false);
    public void hideNonEssentialUI() => _nonEssentialRoamingUI.SetActive(false);
    public void showNonEssentialUI() => _nonEssentialRoamingUI.SetActive(true);
}
