//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: ClaimRewardUI.cs
//  Description: Script for handle claimable item UI in world
//
//  History:
//  - November 5, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClaimRewardUI : MonoBehaviour
{
    public static ClaimRewardUI instance;
    [SerializeField] private Button _claimButton;
    [SerializeField] private GameObject _rewardUI;
    [SerializeField] private RectTransform _itemPost;
    private List<ItemBaseSO> _itemToPool;
    private Dictionary<string, GameObject> _itemDictionary = new Dictionary<string, GameObject>();
    private List<ClaimableItem> _claimableItem;

    [Space(10)]
    [Header("Item Description")]
    [SerializeField] private GameObject _itemDesc;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemInAmountInventory;
    [SerializeField] private Vector2 _descOffside;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _claimButton.onClick.AddListener(() =>
              {
                  closeClaimableUI(_claimableItem);
              });
    }

    // private void Start()
    // {
    //     // _itemToPool = InventoryHandler.instance.getAllItemList();

    //     for (int i = 0; i < _itemToPool.Count; i++)
    //     {
    //         poolAllItem(i);
    //     }
    // }

    private void poolAllItem(int index)
    {
        ItemBaseSO item = _itemToPool[index];

        // // GameObject obj = Instantiate(item.itemObject);

        // // obj.GetComponent<Item>().itemMode = Item.ItemMode.Claimable;

        // obj.SetActive(false);
        // obj.transform.SetParent(_itemPost, false);

        // _itemDictionary[item.itemName] = obj;
    }


    public IEnumerator claimableReward(List<ClaimableItem> list)
    {
        if (list.Any())
        {
            foreach (var item in list)
            {
                if (_itemDictionary.ContainsKey(item.item.itemName))
                {
                    _itemDictionary[item.item.itemName].SetActive(true);
                }
                else
                {
                    Debug.LogError("Item with name " + item.item.itemName + " not found in the dictionary.");
                }
                yield return new WaitForSeconds(.1f);
            }
        }

        CharacterInput.instance.changePlayerState(PlayerState.Reward);
    }

    public void openClaimReward()
    {
        _claimableItem = DropItemHandler.instance.getDroppedItem();
        _rewardUI.SetActive(true);
        StartCoroutine(claimableReward(_claimableItem));
    }

    private void closeClaimableUI(List<ClaimableItem> list)
    {
        if (list.Any())
        {
            foreach (var item in list)
            {
                if (_itemDictionary.ContainsKey(item.item.itemName))
                {
                    _itemDictionary[item.item.itemName].SetActive(false);
                }
                else
                {
                    Debug.LogError("Item with name " + item.item.itemName + " not found in the dictionary.");
                }

            }
        }

        _rewardUI.SetActive(false);
        _itemDesc.SetActive(false);
        CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
    }

    public void showItemDesc(RectTransform itemPost, ItemBaseSO itemSO)
    {
        if (_itemDesc.activeSelf)
            _itemDesc.SetActive(false);

        _itemName.text = itemSO.itemName;
        _itemInAmountInventory.text = "Amount : " + itemSO.amountInInventory.ToString();
        _itemDesc.GetComponent<Image>().rectTransform.anchoredPosition = itemPost.anchoredPosition + _descOffside;

        _itemDesc.SetActive(true);
    }

}
