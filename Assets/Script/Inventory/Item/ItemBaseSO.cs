//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: ItemBaseSO.cs
//  Description: Script for item SO
//
//  History:
//  - September 2, 2023: Created by Bhekti
//  - 
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;

public class ItemBaseSO : ScriptableObject
{
    public string itemId;
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string desc;
    public uint amountInInventory;
    [Space(10)]
    public GenericItem itemObject;

    [Header("Utilities")]
    public bool isStackable;
}