using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonMainMenuAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum ButtonState
    {
        pointerEnter,
        PointerExit,
        PointerClick
    }

    [SerializeField] private GameObject _buttonMarker;
    [SerializeField] private ButtonState _buttonState = ButtonState.PointerExit;
    [HideInInspector] public ButtonState buttonState { get { return _buttonState; } }

    private void OnEnable()
    {
        if (_buttonMarker)
            _buttonMarker.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_buttonMarker)
            _buttonMarker.SetActive(true);
        transform.DOScale(1.1f, .5f);
        _buttonState = ButtonState.pointerEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buttonMarker)
            _buttonMarker.SetActive(false);
        transform.DOScale(1, .5f);
        _buttonState = ButtonState.PointerExit;
    }

    public void OnPointerClick(PointerEventData EventData) // noted : entah kenapa belum bisa berfungsi
    {
        if (_buttonMarker)
            _buttonMarker.SetActive(false);
        transform.DOScale(1, .5f);
        _buttonState = ButtonState.PointerClick;
    }
}