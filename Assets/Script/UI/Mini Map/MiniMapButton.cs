using UnityEngine;
using UnityEngine.UI;

public class MiniMapMapButton : MonoBehaviour
{
    [SerializeField] private Button _mapButton;
    [SerializeField] private MapName _mapName;


    private void Awake()
    {
        _mapButton.onClick.AddListener(() =>
         {
             HUDHandler.instance.interactiveMiniMap.changeMap(_mapName);
         });
    }

}
