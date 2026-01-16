using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoMonitoring : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public string escenaSiguiente = "NombreDeTuEscena";
    public Material skyboxMaterial;
    public float duracionFundido = 1.5f;

    // El método Start() y IniciarFundido() se mantienen igual.

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += IniciarFundido;

        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_Exposure", 1f);
        }
    }

    void IniciarFundido(VideoPlayer vp)
    {
        if (skyboxMaterial != null)
        {
            StartCoroutine(FundidoACambiarEscenaVR());
        }
        else
        {
            // Fallback: si el material no está asignado, carga la escena inmediatamente.
            SceneManager.LoadSceneAsync(escenaSiguiente);
        }
    }

    /// <summary>
    /// Corrutina para esperar 0.5s, realizar el fundido a negro y luego cambiar de escena.
    /// </summary>
    IEnumerator FundidoACambiarEscenaVR()
    {
        // 1. **NUEVO:** Esperar 0.5 segundos ANTES de empezar el fundido.
        yield return new WaitForSeconds(0.5f);

        float tiempoActual = 0f;
        float exposicionInicial = skyboxMaterial.GetFloat("_Exposure");
        float exposicionFinal = 0f;

        while (tiempoActual < duracionFundido)
        {
            tiempoActual += Time.deltaTime;
            float t = tiempoActual / duracionFundido;

            // Interpolación de la exposición del Skybox (de 1 a 0)
            float nuevaExposicion = Mathf.Lerp(exposicionInicial, exposicionFinal, t);
            skyboxMaterial.SetFloat("_Exposure", nuevaExposicion);

            yield return null;
        }

        // 2. Asegura que la exposición sea completamente negra.
        skyboxMaterial.SetFloat("_Exposure", exposicionFinal);

        // 3. Cargar la siguiente escena.
        SceneManager.LoadSceneAsync(escenaSiguiente);
    }
}