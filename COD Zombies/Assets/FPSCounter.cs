using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsText;
    public float smoothing = 0.00000001f; // Valor de suavizado, ajusta según sea necesario

    private float currentFps;

    void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("El objeto de texto (Text) no está asignado en el inspector.");
            enabled = false; // Desactivar el script si el objeto de texto no está asignado.
            return;
        }
    }

    void Update()
    {
        // Calcular los FPS suavizados
        float fps = 1.0f / Time.deltaTime;
        currentFps = Mathf.Lerp(currentFps, fps, smoothing);

        // Actualizar el texto
        fpsText.text = $"{Mathf.Round(currentFps)} FPS";
    }
}



