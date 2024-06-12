//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: itemCrafting.cs
//  Description: Script for item crafting utilities
//
//  History:
//  - November 7, 2023: Created by Bhekti
//  - 
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ItemCrafting : MonoBehaviour
{
    [SerializeField] private CraftingMaterialUI _craftingMaterialUI;
    [System.Serializable]
    public struct Material
    {
        public ItemBaseSO itemSO;
        public uint amount;
    }
    [SerializeField] private Material[] _material = new Material[3];
    [SerializeField] private Button _openMaterial;
    [SerializeField] private ItemBaseSO _itemSO;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDesc;
    // [SerializeField] private TextMeshProUGUI _itemAmount;

    private void Awake()
    {
        _icon.sprite = _itemSO.icon;
        _itemName.text = _itemSO.itemName;
        _itemDesc.text = _itemSO.desc;
        // _itemAmount.text = _itemSO.amountInInventory.ToString();
        _openMaterial.onClick.AddListener(() =>
        {
            _craftingMaterialUI.ShowMaterialDesc(_material, _itemSO);
        });
    }
}