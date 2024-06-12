using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GenericButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private TextMeshProUGUI _text;

    public void BindButton(UnityAction clickCallback)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(clickCallback);
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}