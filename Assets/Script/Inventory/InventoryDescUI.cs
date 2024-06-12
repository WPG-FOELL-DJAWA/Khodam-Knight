//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: InventoryDescUI.cs
//  Description: Script for Inventory description UI
//
//  History:
//  - october 19, 2023: Created by Bhekti
//  - 
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class InventoryDescUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GenericText _name;
    [SerializeField] private GenericText _desc;
    [Space]
    [SerializeField] private GameObject _descObject;
    [SerializeField] private GameObject _ItemStatusTitle;
    [SerializeField] private GameObject _itemStatusPost;
    [SerializeField] private List<GenericItemStatus> _genericItemStatus;



    public void showDesc(ItemBaseSO itemSO)
    {
        _descObject.SetActive(true);
        _name.SetText(itemSO.itemName);
        _desc.SetText(itemSO.desc);
        _icon.sprite = itemSO.icon;

        if (itemSO is ItemEquipmentSO)
        {
            _ItemStatusTitle.SetActive(true);
            _itemStatusPost.SetActive(true);
            var equipment = itemSO as ItemEquipmentSO;
            setStatus(equipment.status.attack, 0, "ATK");
            setStatus(equipment.status.health, 1, "HP");
            setStatus(equipment.status.mana, 2, "MANA");
            setStatus(equipment.status.defense, 3, "DEF");

        }
        else
        {
            _ItemStatusTitle.SetActive(false);
            _itemStatusPost.SetActive(false);
        }
    }

    private void setStatus(uint status, int indexStatus, string name)
    {
        if (status != 0)
        {
            _genericItemStatus[indexStatus].gameObject.SetActive(true);
            _genericItemStatus[indexStatus].setup(name, status.ToString());
        }
        else
        {
            _genericItemStatus[indexStatus].gameObject.SetActive(false);
        }
    }
}
