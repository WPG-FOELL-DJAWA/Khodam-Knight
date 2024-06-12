using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class CharacterSelection : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _allCharacterButton;
    [SerializeField] private Button _character1Button;
    [SerializeField] private Button _character2Button;
    [SerializeField] private Button _character3Button;
    [SerializeField] private Button _character4Button;

    [Header("Marker")]
    [SerializeField] private GameObject _marker1;
    [SerializeField] private GameObject _marker2;
    [SerializeField] private GameObject _marker3;
    [SerializeField] private GameObject _marker4;

    [Header("Virtual Camera")]
    [SerializeField] private GameObject _allCharacterCamera;
    [SerializeField] private GameObject _character1Camera;
    [SerializeField] private GameObject _character2Camera;
    [SerializeField] private GameObject _character3Camera;
    [SerializeField] private GameObject _character4Camera;

    [Header("Equipment")]
    [SerializeField] private GameObject _equipmentSlot;
    private float _timerToOpen = 2;

    [Header("Button")]
    public Vector3 hoverScale;
    public Vector3 normalScale;

    [Header("Marker")]
    public Vector3 clickMarkerScale;
    public Vector3 normalMarkerScale;

    [HideInInspector] public GameObject _clickedButton;
    [HideInInspector] public GameObject _currentMarker;

    private void Awake()
    {
        _allCharacterButton.onClick.AddListener(() =>
       {
           allCharacterSelected();
       });

        _character1Button.onClick.AddListener(() =>
       {
           if (_clickedButton != _character1Button.gameObject)
               character1Selected();
       });

        _character2Button.onClick.AddListener(() =>
        {
            if (_clickedButton != _character2Button.gameObject)
                character2Selected();
        });

        _character3Button.onClick.AddListener(() =>
        {
            if (_clickedButton != _character3Button.gameObject)
                character3Selected();
        });

        _character4Button.onClick.AddListener(() =>
        {
            if (_clickedButton != _character4Button.gameObject)
                character4Selected();
        });
    }

    private void allCharacterSelected()
    {
        _equipmentSlot.SetActive(false);

        _character1Camera.SetActive(false);
        _character2Camera.SetActive(false);
        _character3Camera.SetActive(false);
        _character4Camera.SetActive(false);

        _allCharacterCamera.SetActive(true);
    }

    private void character1Selected()
    {
        _equipmentSlot.SetActive(false);

        _allCharacterCamera.SetActive(false);
        _character2Camera.SetActive(false);
        _character3Camera.SetActive(false);
        _character4Camera.SetActive(false);

        unScaleMarker();
        _character1Button.transform.DOScale(normalScale, 1)
        .SetEase(Ease.InOutSine);

        _marker1.transform.DOScale(clickMarkerScale, 1);

        _clickedButton = _character1Button.gameObject;
        _currentMarker = _marker1;

        _character1Camera.SetActive(true);
        StartCoroutine(countdownTimer(_timerToOpen));
    }

    private void character2Selected()
    {
        _equipmentSlot.SetActive(false);

        _allCharacterCamera.SetActive(false);
        _character1Camera.SetActive(false);
        _character3Camera.SetActive(false);
        _character4Camera.SetActive(false);

        unScaleMarker();
        _character2Button.transform.DOScale(normalScale, 1)
        .SetEase(Ease.InOutSine);

        _marker2.transform.DOScale(clickMarkerScale, 1);

        _clickedButton = _character2Button.gameObject;
        _currentMarker = _marker2;

        _character2Camera.SetActive(true);
        StartCoroutine(countdownTimer(_timerToOpen));
    }

    private void character3Selected()
    {
        _equipmentSlot.SetActive(false);

        _allCharacterCamera.SetActive(false);
        _character1Camera.SetActive(false);
        _character2Camera.SetActive(false);
        _character4Camera.SetActive(false);

        unScaleMarker();
        _character3Button.transform.DOScale(normalScale, 1)
        .SetEase(Ease.InOutSine);

        _marker3.transform.DOScale(clickMarkerScale, 1);

        _clickedButton = _character3Button.gameObject;
        _currentMarker = _marker3;

        _character3Camera.SetActive(true);
        StartCoroutine(countdownTimer(_timerToOpen));
    }

    private void character4Selected()
    {
        _equipmentSlot.SetActive(false);

        _allCharacterCamera.SetActive(false);
        _character1Camera.SetActive(false);
        _character2Camera.SetActive(false);
        _character3Camera.SetActive(false);

        unScaleMarker();
        _character4Button.transform.DOScale(normalScale, 1)
        .SetEase(Ease.InOutSine);

        _marker4.transform.DOScale(clickMarkerScale, 1);

        _clickedButton = _character4Button.gameObject;
        _currentMarker = _marker4;

        _character4Camera.SetActive(true);
        StartCoroutine(countdownTimer(_timerToOpen));
    }


    private void unScaleMarker()
    {
        if (_currentMarker != null)
        {
            _currentMarker.transform.DOScale(normalMarkerScale, 2);
        }
    }

    private IEnumerator countdownTimer(float time)
    {
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1f);

        }
        _equipmentSlot.SetActive(true);
    }

}
