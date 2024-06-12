using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Bhekti/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemBaseSO> allItem;
    public Dictionary<string, ItemBaseSO> itemDictionary = new Dictionary<string, ItemBaseSO>();

    // public SerializableDictionary<string, uint> playerItem = new SerializableDictionary<string, uint>();
}