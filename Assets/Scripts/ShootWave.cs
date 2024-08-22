using UnityEngine;
using System.Collections;

public class ShootWave : MonoBehaviour
{
    public Transform childObject; // El objeto hijo que cambiará de tamaño
    public Vector3 initialScale = Vector3.one; // Tamaño inicial del objeto
    public Vector3 finalScale = Vector3.one; // Tamaño final del objeto
    public float scaleUpTime = 1.0f; // Tiempo para escalar hacia arriba
    public float scaleDownTime = 0.5f; // Tiempo para escalar hacia abajo
    public int waveDamage = 10;

    public void waveGrowth()
    {
        StartCoroutine(ScaleUpAndDown(childObject, initialScale, finalScale, scaleUpTime, scaleDownTime));
    }

    IEnumerator ScaleUpAndDown(Transform target, Vector3 initial, Vector3 final, float upTime, float downTime)
    {
        yield return StartCoroutine(ScaleOverTime(target, initial, final, upTime));
        yield return StartCoroutine(ScaleOverTime(target, final, initial, downTime));
    }

    IEnumerator ScaleOverTime(Transform target, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = endScale; // Asegurarse de que el tamaño final sea exacto
    }

    public float currentArea()
    {
        return finalScale.x;
    }
}