using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    void Update()
    {
        // Merotasi objek di sekitar sumbu y (atau sesuaikan sumbu yang diinginkan)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
