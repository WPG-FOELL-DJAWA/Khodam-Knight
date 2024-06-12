using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CharacterSelection _characterSelection;
    [SerializeField] private RectTransform _rectTransform;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_characterSelection._clickedButton != gameObject)
        {
            transform.DOScale(_characterSelection.hoverScale, 1);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_characterSelection._clickedButton != gameObject)
        {
            transform.DOScale(_characterSelection.normalScale, 1);
        }
    }
}
