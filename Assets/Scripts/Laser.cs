using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float rotationAngle = 20f; // Grados de rotaci�n
    public float rotationTime = 0.5f; // Tiempo de rotaci�n en segundos

    void Start()
    {
        StartCoroutine(RotateOverTime(rotationAngle, rotationTime));
    }

    IEnumerator RotateOverTime(float angle, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, 0, -angle); // Rotaci�n en sentido horario

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Asegurarse de que la rotaci�n final sea exacta
        Destroy(gameObject);
    }
}