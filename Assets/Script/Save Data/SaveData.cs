using System.Collections;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private GameObject _UISave;
    private SaveDataInSlot selectedSlot;
    private string selectedSlotName;

    public void saveCurrentData(SaveDataInSlot saveDataInSlot, string slotName)
    {
        selectedSlot = saveDataInSlot;
        selectedSlotName = slotName;
        _UISave.SetActive(false);
        _UI.SetActive(false);
        StartCoroutine(Screenshot());
    }

    public IEnumerator Screenshot()
    {

        yield return new WaitForEndOfFrame();
        string folderPath = Application.persistentDataPath + "/Save Data/" + selectedSlotName;
        var folder = Directory.CreateDirectory(folderPath);

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        string name = "Thumbnail.png";

        //PC
        byte[] bytes = texture.EncodeToPNG();
        string imagePath = folderPath + "/" + name;
        //_imagePath = Application.dataPath + "/../" + name;
        File.WriteAllBytes(imagePath, bytes);
        Debug.Log(Application.persistentDataPath);
        Destroy(texture);

        _UISave.SetActive(true);
        _UI.SetActive(true);
        selectedSlot.saveDataDone(imagePath);
    }

}
