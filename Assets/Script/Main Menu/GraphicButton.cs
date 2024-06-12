using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicButton : MonoBehaviour
{
    [SerializeField] private GenericButton genericButton;
    [SerializeField] private string graphicName;
    [Header("View")]
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Image image;

    [Header("sprite")]
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite normalSprite;

    private void Start()
    {
        genericButton.BindButton(() => SetGraphic());
    }

    private void Update()
    {
        if (SettingHandler.instance.CurrentQualityName == graphicName)
        {
            image.sprite = selectedSprite;
            textMeshProUGUI.color = new Color(0, 0, 0, 255);
        }
        else
        {
            image.sprite = normalSprite;
            textMeshProUGUI.color = new Color(255, 255, 255, 55);
        }
    }

    private void SetGraphic()
    {
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            if (graphicName == QualitySettings.names[i])
            {
                QualitySettings.SetQualityLevel(i);
                SettingHandler.instance.CurrentQualityName = QualitySettings.names[i];
                Debug.Log(QualitySettings.names[i]);
            }
        }
    }
}
