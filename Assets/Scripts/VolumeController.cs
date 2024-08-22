using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Referencia al slider en la UI
    public AudioSource audioSource; // Referencia al AudioSource

    void Start()
    {
        // Configura el slider para reflejar el volumen inicial del AudioSource
        if (audioSource != null)
        {
            volumeSlider.value = audioSource.volume;
        }

        // Añade un listener para llamar al método OnVolumeChange cuando el valor del slider cambie
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
    }
}
