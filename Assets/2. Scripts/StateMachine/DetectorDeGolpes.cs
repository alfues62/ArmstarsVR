using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DetectorDeGolpes : MonoBehaviour
{
    public bool golpeRegistrado = false;

    [Header("Configuración de Sonido")]
    public AudioSource audioSource;

    private float velocidadGuardada;
    private Vector3 puntoLocalGuardado;

    void Awake()
    {
        // Usamos Awake para asegurar que la referencia esté lista antes de cualquier colisión
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (golpeRegistrado) return;

        // Reproducir sonido
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        velocidadGuardada = collision.relativeVelocity.magnitude;
        Vector3 puntoImpactoGlobal = collision.contacts[0].point;
        puntoLocalGuardado = transform.InverseTransformPoint(puntoImpactoGlobal);

        Debug.Log($"[Detector] ¡IMPACTO! Vel: {velocidadGuardada:F1}");
        golpeRegistrado = true;
    }

    public bool IntentarObtenerGolpe(out float velocidad, out Vector3 puntoLocal)
    {
        if (golpeRegistrado)
        {
            velocidad = velocidadGuardada;
            puntoLocal = puntoLocalGuardado; // CORREGIDO: ahora coincide con el parámetro de salida
            return true;
        }
        else
        {
            velocidad = 0f;
            puntoLocal = Vector3.zero;
            return false;
        }
    }

    public void PrepararNuevoTurno()
    {
        golpeRegistrado = false;
    }
}