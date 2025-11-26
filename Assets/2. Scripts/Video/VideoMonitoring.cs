using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class VideoMonitoring : MonoBehaviour
{
    // El componente VideoPlayer del objeto
    private VideoPlayer videoPlayer;

    // Nombre de la escena a la que quieres pasar
    public string escenaSiguiente = "EscenaMaquinaExp";

    void Start()
    {
        // 1. Obtener el componente VideoPlayer
        videoPlayer = GetComponent<VideoPlayer>();

        // 2. Suscribir el método 'VideoTerminado' al evento de fin de bucle
        videoPlayer.loopPointReached += VideoTerminado; // 
    }

    /// <summary>
    /// Este método se llama automáticamente cuando la reproducción del video llega al final.
    /// </summary>
    void VideoTerminado(VideoPlayer vp)
    {
        // Carga la nueva escena de forma asíncrona 
        SceneManager.LoadSceneAsync(escenaSiguiente);
    }
}