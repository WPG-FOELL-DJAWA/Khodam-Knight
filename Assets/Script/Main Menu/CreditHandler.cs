using UnityEngine;
using DG.Tweening;

public class CreditHandler : MonoBehaviour
{
    [SerializeField] private GameObject _creditUIPanel;
    [SerializeField] private GenericButton _closeButton;

    private void OnEnable()
    {
        _closeButton.BindButton(() => closeSetting());
        settingShow();
    }

    private void settingShow()
    {
        _creditUIPanel.transform.localScale = Vector3.zero;
        _creditUIPanel.transform.DOScale(1, 2)
        .SetEase(Ease.Linear);
    }

    private void closeSetting()
    {
        _creditUIPanel.transform.DOScale(0, 2)
        .SetEase(Ease.Linear);
        _creditUIPanel.SetActive(false);
        MainMenuHandler.instance.closeSettingOrCredit();
    }
}
