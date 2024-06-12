using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Image _loadingKey;
    [SerializeField] private float _duration;
    [SerializeField] private float _currentTime;
    private bool _startTimer = true;

    private void Start()
    {
        _loadingKey.fillAmount = 0;
    }

    private void Update()
    {
        if (gameObject.activeSelf && _startTimer)
        {
            StartCoroutine(updateTimer()); // noted : perlu perbaiki lagi agar lebih efektif
            _startTimer = false;
        }
    }


    private IEnumerator updateTimer()
    {
        while (_currentTime < _duration) // noted : mungkin harus pakai delta time untuk pause timer saat time scale 0
        {
            _currentTime++;
            _loadingKey.fillAmount = _currentTime / _duration;
            yield return new WaitForSecondsRealtime(1f);

        }
        _loadingKey.fillAmount = 0;
        TutorialHandler.instance.closeCurrentTutorial();
    }



}
