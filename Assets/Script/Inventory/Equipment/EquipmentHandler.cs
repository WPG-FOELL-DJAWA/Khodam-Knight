using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    public static EquipmentHandler instance;
    [SerializeField] private ItemDatabase _itemDatabase;
    private Dictionary<string, GameObject> _allItemInInventory = new Dictionary<string, GameObject>();

    [Header("equipment Post")]
    [SerializeField] private RectTransform _armorPost;
    [SerializeField] private RectTransform _weaponPost;
    [SerializeField] private RectTransform _accessoryPost;

    [Header("Equipment Slot")]
    [SerializeField] private EquipmentSlot hat;
    [SerializeField] private EquipmentSlot top;
    [SerializeField] private EquipmentSlot bottom;
    [SerializeField] private EquipmentSlot shoes;
    [SerializeField] private EquipmentSlot weapon;
    [SerializeField] private EquipmentSlot ring;
    [SerializeField] private EquipmentSlot bracelet;
    [SerializeField] private EquipmentSlot necklace;

    [Header("Utilities")]
    [SerializeField] private GenericButton armorButton;
    [SerializeField] private GenericButton weaponButton;
    [SerializeField] private GenericButton accessoryButton;
    [SerializeField] private GenericButton confirmButton;
    private GameObject _currentOpenedEquipment;
    private ItemEquipmentSO selectedItem;




    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        armorButton.BindButton(openArmor);
        weaponButton.BindButton(openWeapon);
        accessoryButton.BindButton(openAccessory);
        confirmButton.BindButton(confirmEquip);

        foreach (var item in _itemDatabase.itemDictionary)
        {
            if (item.Value is ItemEquipmentSO)
            {
                poolAllItem(item.Value as ItemEquipmentSO);
            }
        }
    }

    private void poolAllItem(ItemEquipmentSO itemEquipmentSO)
    {
        GameObject obj = Instantiate(itemEquipmentSO.itemObject.gameObject);


        if (itemEquipmentSO.equipmentType == EquipmentType.Necklace)
            obj.transform.SetParent(_accessoryPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Bracelet)
            obj.transform.SetParent(_accessoryPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Ring)
            obj.transform.SetParent(_accessoryPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Weapon)
            obj.transform.SetParent(_weaponPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Bottom)
            obj.transform.SetParent(_armorPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Shoes)
            obj.transform.SetParent(_armorPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Top)
            obj.transform.SetParent(_armorPost, false);

        if (itemEquipmentSO.equipmentType == EquipmentType.Hat)
            obj.transform.SetParent(_armorPost, false);


        obj.GetComponent<GenericItem>().setup(itemEquipmentSO);
        obj.GetComponent<GenericButton>().BindButton(() => selectEquipment(itemEquipmentSO));

        if (itemEquipmentSO.amountInInventory > 0)
        {
            obj.SetActive(true);
        }

        _allItemInInventory.Add(itemEquipmentSO.itemId, obj);
    }

    public void openEquipmentByType(EquipmentType equipmentType)
    {
        _currentOpenedEquipment?.SetActive(false);

        if (equipmentType == EquipmentType.Necklace || equipmentType == EquipmentType.Bracelet || equipmentType == EquipmentType.Ring)
        {
            _accessoryPost.gameObject.SetActive(true);
            _currentOpenedEquipment = _accessoryPost.gameObject;
        }

        if (equipmentType == EquipmentType.Weapon)
        {
            _weaponPost.gameObject.SetActive(true);
            _currentOpenedEquipment = _weaponPost.gameObject;
        }

        if (equipmentType == EquipmentType.Bottom || equipmentType == EquipmentType.Shoes || equipmentType == EquipmentType.Top || equipmentType == EquipmentType.Hat)
        {
            _armorPost.gameObject.SetActive(true);
            _currentOpenedEquipment = _armorPost.gameObject;
        }
    }

    private void selectEquipment(ItemEquipmentSO itemEquipmentSO)
    {
        selectedItem = itemEquipmentSO;
        confirmButton.gameObject.SetActive(true);
    }

    public void confirmEquip()
    {
        confirmButton.gameObject.SetActive(false);

        if (selectedItem.equipmentType == EquipmentType.Necklace)
        {
            necklace.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Bracelet)
        {
            bracelet.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Ring)
        {
            ring.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Weapon)
        {
            weapon.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Bottom)
        {
            bottom.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Shoes)
        {
            shoes.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Top)
        {
            top.setUsedEquipment(selectedItem, selectedItem.icon);
        }

        if (selectedItem.equipmentType == EquipmentType.Hat)
        {
            hat.setUsedEquipment(selectedItem, selectedItem.icon);
        }
    }

    public void openArmor()
    {
        _currentOpenedEquipment?.SetActive(false);

        _armorPost.gameObject.SetActive(true);
        _currentOpenedEquipment = _armorPost.gameObject;
    }

    public void openWeapon()
    {
        _currentOpenedEquipment?.SetActive(false);

        _weaponPost.gameObject.SetActive(true);
        _currentOpenedEquipment = _weaponPost.gameObject;
    }

    public void openAccessory()
    {
        _currentOpenedEquipment?.SetActive(false);

        _accessoryPost.gameObject.SetActive(true);
        _currentOpenedEquipment = _accessoryPost.gameObject;
    }
}
