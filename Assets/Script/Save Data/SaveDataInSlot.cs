using UnityEngine;

public class SaveDataInSlot : MonoBehaviour
{
    [SerializeField] private SaveData _SaveData;
    [SerializeField] private string _slotName;
    [SerializeField] private GameObject _emptyData;
    [SerializeField] private GameObject _filledData;


    [SerializeField] private LoadDataImage _loadDataImage;



    public void saveDataInSlot()
    {
        _SaveData.saveCurrentData(this, _slotName);
    }

    public void saveDataDone(string imagePath)
    {
        _emptyData.SetActive(false);
        _filledData.SetActive(true);
        _loadDataImage.LoadImage(imagePath);
    }
}
