using UnityEngine;

public class InitializeItemDatabase : MonoBehaviour
{
    [SerializeField] private ItemDatabase _dictionaryData;

    private void Awake()
    {
        var allItem = _dictionaryData.allItem;
        var dictionaryItem = _dictionaryData.itemDictionary;

        foreach (var item in allItem)
        {
            dictionaryItem.Add(item.itemId, item);
        }
    }
}
