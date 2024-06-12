using UnityEngine.UI;
using UnityEngine;

public class LoadDataImage : MonoBehaviour
{
    public RawImage rawImage;

    public void LoadImage(string imagePath)
    {
        Texture2D texture = LoadTextureFromFile(imagePath);

        if (texture != null)
        {

            rawImage.texture = texture;
        }
        else
        {
            Debug.LogError("Gagal memuat gambar dari path: " + imagePath);
        }
    }

    private Texture2D LoadTextureFromFile(string path)
    {

        byte[] fileData = System.IO.File.ReadAllBytes(path);


        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }
}
