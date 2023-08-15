using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Ось вращения (по умолчанию - вокруг оси Y)
    public float rotationSpeed = 30.0f; // Скорость вращения в градусах в секунду

    private void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
