//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: item.cs
//  Description: Script for item utilities
//
//  History:
//  - october 19, 2023: Created by Bhekti
//  - 
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericItem : MonoBehaviour
{
    [SerializeField] private GenericButton _itemButton;
    [SerializeField] private Image _icon;
    [SerializeField] private GenericText _itemAmount;


    private ItemBaseSO _itemSO;

    public void setup(ItemBaseSO itemSO)
    {
        _itemSO = itemSO;
        _icon.sprite = _itemSO.icon;
        _itemAmount.SetText(_itemSO.amountInInventory.ToString());
    }

    public void UpdateAmount()
    {
        _itemAmount.SetText(_itemSO.amountInInventory.ToString());
    }
}