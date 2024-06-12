//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: HUDHandler.cs
//  Description: Script for handle HUD in world
//
//  History:
//  - October 19, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    public static HUDHandler instance;
    [SerializeField] private PlayerSO _playerSO;

    [Header("All UI Handler")]
    public RoamingUIHandler roamingUIHandler;
    public InventoryHandler inventoryHandler;
    public InteractiveMiniMap interactiveMiniMap;
    public QuestHandler questHandler;
    public CombatUIHandler combatUIHandler;
    [Space]
    [SerializeField] private MapName characterInfoScene;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


    }

    private void Start()
    {
        hideAllUIGroup();
    }

    public void openInventory()
    {
        inventoryHandler.openInventory();

        CharacterInput.instance.changePlayerState(PlayerState.Inventory);
    }

    public void closeInventory()
    {
        inventoryHandler.closeInventory();

        CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
    }

    public void openMiniMap()
    {
        interactiveMiniMap.openInteractiveMap();

        CharacterInput.instance.changePlayerState(PlayerState.Minimap);
    }

    public void closeMiniMap()
    {
        interactiveMiniMap.closeInteractiveMap();

        CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
    }

    public void openQuest()
    {
        questHandler.openQuestJournal();

        CharacterInput.instance.changePlayerState(PlayerState.Quest);
    }
    public void closeQuest()
    {
        questHandler.closeQuestJournal();

        CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
    }

    public void openCharacterInfo()
    {
        _playerSO.lastScene = RoamingMapData.instance.getMapName();
        _playerSO.lastPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        hideAllUIGroup();
        LoadScene.instance.loadScene(characterInfoScene);
    }


    public void hideAllUIGroup()
    {
        roamingUIHandler.gameObject.SetActive(false);
        inventoryHandler.gameObject.SetActive(false);
        interactiveMiniMap.gameObject.SetActive(false);
        questHandler.gameObject.SetActive(false);
        combatUIHandler.gameObject.SetActive(false);
    }

    public void showAllUIGroup()
    {
        roamingUIHandler.gameObject.SetActive(true);
        inventoryHandler.gameObject.SetActive(true);
        interactiveMiniMap.gameObject.SetActive(true);
        questHandler.gameObject.SetActive(true);
        combatUIHandler.gameObject.SetActive(true);
    }
}