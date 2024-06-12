using UnityEngine;
using UnityEngine.UI;

public class QuestFilterButton : MonoBehaviour
{
    [SerializeField] private Button _allQuestButton;
    [SerializeField] private Button _mainQuestButton;
    [SerializeField] private Button _sideQuestButton;
    [SerializeField] private GameObject _allQuestList;
    [SerializeField] private GameObject _MainQuestList;
    [SerializeField] private GameObject _sideQuestList;
    private GameObject _currentQuestList;

    private void Awake()
    {
        _currentQuestList = _allQuestList;

        _allQuestButton.onClick.AddListener(() =>
       {
           _currentQuestList.SetActive(false);
           _allQuestList.SetActive(true);
           _currentQuestList = _allQuestList;
       });

        _mainQuestButton.onClick.AddListener(() =>
        {
            _currentQuestList.SetActive(false);
            _MainQuestList.SetActive(true);
            _currentQuestList = _MainQuestList;
        });

        _sideQuestButton.onClick.AddListener(() =>
       {
           _currentQuestList.SetActive(false);
           _sideQuestList.SetActive(true);
           _currentQuestList = _sideQuestList;
       });
    }
}