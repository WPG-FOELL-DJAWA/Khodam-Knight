using UnityEngine;
using UnityEngine.UI;

public class MiniMapIcon : MonoBehaviour
{
    [SerializeField] private Button _miniMapButton;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _iconName;
    [TextArea]
    [SerializeField] private string _iconDesc;

    [Space]
    [Header("Only check if icon is teleport")]
    [SerializeField] private bool _isTeleport;
    [SerializeField] private MapName _targetTeleportScene;
    [SerializeField] private Vector3 _targetPost;

    private void Awake()
    {
        _miniMapButton.onClick.AddListener(() =>
        {
            showIconDesc();
        });
    }

    public void showIconDesc()
    {
        HUDHandler.instance.interactiveMiniMap.openIcon(_iconSprite, _iconName, _iconDesc, _isTeleport, _targetTeleportScene, _targetPost);
    }
}
