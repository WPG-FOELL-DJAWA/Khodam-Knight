//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: InventoryHandler.cs
//  Description: Script for handle UI inventory
//
//  History:
//  - October 26, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private InventoryDescUI _inventoryDescUI;
    [SerializeField] private GameObject _inventoryObject;
    [SerializeField] private GameObject _itemDesc;
    [SerializeField] private ItemDatabase _itemDatabase;
    private Dictionary<string, GameObject> _allItemInInventory = new Dictionary<string, GameObject>();

    private List<ItemBaseSO> _consumableItemActiveInInventory = new List<ItemBaseSO>();
    private List<ItemBaseSO> _equipmentItemActiveInInventory = new List<ItemBaseSO>();
    private List<ItemBaseSO> _materialItemActiveInInventory = new List<ItemBaseSO>();
    private List<ItemBaseSO> _questItemActiveInInventory = new List<ItemBaseSO>();

    public List<ItemBaseSO> consumableItemActiveInInventory { get { return _consumableItemActiveInInventory; } }
    public List<ItemBaseSO> equipmentItemActiveInInventory { get { return _equipmentItemActiveInInventory; } }
    public List<ItemBaseSO> materialItemActiveInInventory { get { return _materialItemActiveInInventory; } }
    public List<ItemBaseSO> questItemActiveInInventory { get { return _questItemActiveInInventory; } }

    [Space(10)]
    [SerializeField] private RectTransform _consumablePost;
    [SerializeField] private RectTransform _equipmentPost;
    [SerializeField] private RectTransform _materialPost;
    [SerializeField] private RectTransform _questPost;

    [Space(10)]
    [Header("Inventory Object")]
    [SerializeField] private GameObject _consumableInventory;
    [SerializeField] private GameObject _equipmentInventory;
    [SerializeField] private GameObject _materialInventory;
    [SerializeField] private GameObject _questInventory;
    [SerializeField] private GameObject _currentInventory;

    [Space(10)]
    [Header("Navigation Button")]
    [SerializeField] private GenericButton _consumableButton;
    [SerializeField] private GenericButton _equipmentButton;
    [SerializeField] private GenericButton _materialButton;
    [SerializeField] private GenericButton _questButton;
    [SerializeField] private Image _currentButton;

    [Space(10)]
    [Header("Utilities")]
    [SerializeField] private GenericText _itemInInventory;
    [SerializeField] private uint _inventoryLimit;

    private void Start()
    {
        _consumableButton.BindButton(() => changeInventory(_consumableItemActiveInInventory, _consumableInventory, _consumableButton));
        _equipmentButton.BindButton(() => changeInventory(_equipmentItemActiveInInventory, _equipmentInventory, _equipmentButton));
        _materialButton.BindButton(() => changeInventory(_materialItemActiveInInventory, _materialInventory, _materialButton));
        _questButton.BindButton(() => changeInventory(_questItemActiveInInventory, _questInventory, _questButton));
        setTotalItemInInventory(_consumableItemActiveInInventory);

        foreach (var item in _itemDatabase.itemDictionary)
        {
            poolAllItem(item.Value);
        }
    }





    private void closeDesc()
    {
        _itemDesc.SetActive(false);
    }

    private void poolAllItem(ItemBaseSO itemBaseSO)
    {
        GameObject obj = Instantiate(itemBaseSO.itemObject.gameObject);


        if (itemBaseSO.itemType == ItemType.Consumable)
            obj.transform.SetParent(_consumablePost, false);

        if (itemBaseSO.itemType == ItemType.Equipment)
            obj.transform.SetParent(_equipmentPost, false);

        if (itemBaseSO.itemType == ItemType.Material)
            obj.transform.SetParent(_materialPost, false);

        if (itemBaseSO.itemType == ItemType.Quest)
            obj.transform.SetParent(_questPost, false);


        obj.GetComponent<GenericItem>().setup(itemBaseSO);
        obj.GetComponent<GenericButton>().BindButton(() => openItemDesc(itemBaseSO));

        if (itemBaseSO.amountInInventory > 0)
        {
            obj.SetActive(true);
            addItemToList(itemBaseSO);
        }
        else
        {
            obj.SetActive(false);
        }


        _allItemInInventory.Add(itemBaseSO.itemId, obj);
    }

    public void AddItem(string id, uint amount)
    {
        if (amount != 0)
        {
            if (_itemDatabase.itemDictionary[id])
            {
                if (_itemDatabase.itemDictionary[id].amountInInventory == 0)
                    addItemToList(_itemDatabase.itemDictionary[id]);

                _itemDatabase.itemDictionary[id].amountInInventory += amount;
                _allItemInInventory[id].GetComponent<GenericItem>().UpdateAmount();

                if (!_allItemInInventory[id].activeSelf)
                    _allItemInInventory[id].SetActive(true);
            }
            else
            {
                Debug.LogError("Item with name " + id + " not found in the dictionary.");
            }
        }
        else
        {
            Debug.LogError("cannot add 0");
        }

    }

    public void decreaseItem(string id, uint amount)
    {
        if (amount != 0)
        {
            if (_itemDatabase.itemDictionary[id])
            {
                if (amount > _itemDatabase.itemDictionary[id].amountInInventory)
                {
                    _itemDatabase.itemDictionary[id].amountInInventory -= amount;
                    _allItemInInventory[id].GetComponent<GenericItem>().UpdateAmount();

                    if (_itemDatabase.itemDictionary[id].amountInInventory == 0)
                    {
                        removeItemFromList(_itemDatabase.itemDictionary[id]);
                        _allItemInInventory[id].SetActive(false);
                        _allItemInInventory.Remove(id);
                    }

                }
                else
                    Debug.LogError("Decrease cannot more than amount in inventory");
            }
            else
            {
                Debug.LogError("Item with name " + id + " not found in the dictionary.");
            }
        }
        else
        {
            Debug.LogError("Cannot decrease by 0");
        }
    }

    public void upgradeInventory(uint upgrade)
    {
        _inventoryLimit += upgrade;
    }

    private void setTotalItemInInventory(List<ItemBaseSO> itemList) // noted : hanya update saat start dan berubah inventory. tidak update saat menambah/mengurangi item
    {
        _itemInInventory.SetText(itemList.Count.ToString() + "/" + _inventoryLimit.ToString());
    }


    private void addItemToList(ItemBaseSO item)
    {
        if (item.itemType == ItemType.Consumable)
        {
            _consumableItemActiveInInventory.Add(item);
            return;
        }

        if (item.itemType == ItemType.Equipment)
        {
            _equipmentItemActiveInInventory.Add(item);
            return;
        }

        if (item.itemType == ItemType.Material)
        {
            _materialItemActiveInInventory.Add(item);
            return;
        }

        if (item.itemType == ItemType.Quest)
        {
            _questItemActiveInInventory.Add(item);
            return;
        }
    }

    private void removeItemFromList(ItemBaseSO item)
    {
        if (item.itemType == ItemType.Consumable)
        {
            _consumableItemActiveInInventory.Remove(item);
            return;
        }

        if (item.itemType == ItemType.Equipment)
        {
            _equipmentItemActiveInInventory.Remove(item);
            return;
        }

        if (item.itemType == ItemType.Material)
        {
            _materialItemActiveInInventory.Remove(item);
            return;
        }

        if (item.itemType == ItemType.Quest)
        {
            _questItemActiveInInventory.Remove(item);
            return;
        }
    }

    private void changeInventory(List<ItemBaseSO> itemList, GameObject inventory, GenericButton genericButton)
    {
        //close current opened inventory
        closeDesc();
        _currentInventory.SetActive(false);

        inventory.SetActive(true);
        setTotalItemInInventory(itemList);
        _currentInventory = inventory;

        genericButton.gameObject.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
        _currentButton.color = new Color32(255, 255, 255, 255);

        _currentButton = genericButton.gameObject.GetComponent<Image>();

    }
    private void openItemDesc(ItemBaseSO itemBaseSO)
    {
        _inventoryDescUI.showDesc(itemBaseSO);
    }

    public void openInventory()
    {
        setTotalItemInInventory(_consumableItemActiveInInventory);
        _inventoryObject.SetActive(true);
    }

    public void closeInventory()
    {
        _inventoryObject.SetActive(false);
        closeDesc();
    }
}