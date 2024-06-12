using UnityEngine;

public class RotateAndTintSkybox : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float colorChangeSpeed = 0.5f;

    void Update()
    {
        // Mendapatkan waktu saat ini dalam detik
        float currentTime = Time.time;

        // Menghitung sudut rotasi berdasarkan waktu dan kecepatan rotasi
        float rotationAngle = currentTime * rotationSpeed;

        // Mengatur rotasi skybox
        RenderSettings.skybox.SetFloat("_Rotation", rotationAngle);

        // Menghitung nilai t dari 0 hingga 1 berdasarkan waktu
        float t = Mathf.PingPong(currentTime * colorChangeSpeed, 1f);

        // Definisikan warna langit seperti biru muda, jingga, biru tua, dan biru muda pagi
        Color color1 = new Color(0.5f, 0.7f, 1.0f);   // biru muda
        Color color2 = new Color(1.0f, 0.7f, 0.5f);   // jingga
        Color color3 = new Color(0.1f, 0.2f, 0.4f);   // biru tua
        Color color4 = new Color(0.5f, 0.7f, 1.0f);   // biru muda pagi

        // Mengatur tint color skybox berdasarkan nilai t
        Color lerpedColor = Color.Lerp(color1, color2, t);
        lerpedColor = Color.Lerp(lerpedColor, color3, t);
        lerpedColor = Color.Lerp(lerpedColor, color4, t);

        RenderSettings.skybox.SetColor("_Tint", lerpedColor);
    }
}
