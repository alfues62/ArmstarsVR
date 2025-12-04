using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class CambiarEscena : MonoBehaviour
{
    // Escribe el nombre exacto de la escena en el Inspector
    public string nombreDeLaEscena;

    public void CambiadorEscena()
    {
        if (!string.IsNullOrEmpty(nombreDeLaEscena))
        {
            SceneManager.LoadScene(nombreDeLaEscena);
        }
        else
        {
            Debug.LogError("¡El nombre de la escena está vacío!");
        }
    }
}