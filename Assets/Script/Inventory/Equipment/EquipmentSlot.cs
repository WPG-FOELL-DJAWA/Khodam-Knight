using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private EquipmentType _slotType;
    [SerializeField] private GenericButton _slotButton;
    [SerializeField] private Image _icon;
    [SerializeField] private ItemBaseSO _usedEquipment;

    private void Start()
    {
        _slotButton.BindButton(() => openEquipmentSlot());
    }

    private void openEquipmentSlot()
    {
        EquipmentHandler.instance.openEquipmentByType(_slotType);
    }

    public void setUsedEquipment(ItemBaseSO itemBaseSO, Sprite icon)
    {
        _usedEquipment = itemBaseSO;
        _icon.sprite = icon;
        _icon.color = new Color32(255, 255, 255, 255);
    }
}
