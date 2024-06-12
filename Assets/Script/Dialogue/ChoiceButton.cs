using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public Button button;
    [SerializeField] private GameObject _buttonHover;
    public UnityEvent<int> clickEvent = new ActivateChoiceIndexEvent();

    private class ActivateChoiceIndexEvent : UnityEvent<int>
    {
    }

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            clickEvent.Invoke(transform.GetSiblingIndex());
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonHover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonHover.SetActive(false);
    }

}

