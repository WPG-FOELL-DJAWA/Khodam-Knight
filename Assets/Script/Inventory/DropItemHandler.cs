//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: DropItemHandler.cs
//  Description: Script for Drop item after turn base battle
//
//  History:
//  - October 19, 2023: Created by Bhekti
//  - 
//
//////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class DropItemHandler : MonoBehaviour
{
    public static DropItemHandler instance;
    [SerializeField] private PlayerSO _playerSO;
    private List<ClaimableItem> _droppedItem;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void setDropItem(List<ClaimableItem> claimableItem)
    {
        _droppedItem = claimableItem;
    }

    public List<ClaimableItem> getDroppedItem()
    {
        return _droppedItem;
    }

    public IEnumerator dropItem()
    {
        if (_droppedItem.Any())
        {
            foreach (var item in _droppedItem)
            {
                item.item.amountInInventory += item.amount;
                yield return new WaitForSeconds(.1f);
            }
        }

        LoadScene.instance.loadScene(_playerSO.lastScene);
    }

}
