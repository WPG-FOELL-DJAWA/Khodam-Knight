using UnityEngine;

[CreateAssetMenu(fileName = "ItemEquipmentSO", menuName = "Item/Item Equipment")]
public class ItemEquipmentSO : ItemBaseSO
{
    [Header("Equipment Field")]
    public EquipmentType equipmentType;

    [System.Serializable]
    public struct Status
    {
        public uint attack;
        public uint health;
        public uint mana;
        public uint defense;
    }

    public Status status;
}
