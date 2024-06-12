using UnityEngine;
using TMPro;

public class GenericText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private string _prefix;

    public void SetText(string text)
    {
        _text.text = $"{_prefix} {text}".Trim();
    }
}