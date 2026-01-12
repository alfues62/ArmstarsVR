using UnityEngine;
using System.Collections; // NECESARIO para que funcionen las corrutinas

[RequireComponent(typeof(AudioSource))]
public class DetectorDeGolpes : MonoBehaviour
{
    public bool golpeRegistrado = false;

    [Header("Configuración de Sonido")]
    public AudioSource audioSource;

    [Header("Configuración de VFX")]
    public GameObject vfxGolpe;      // Arrastra aquí el objeto del efecto (ej. una explosión o chispas)
    public float duracionVFX = 1.0f; // Tiempo que estará visible

    private float velocidadGuardada;
    private Vector3 puntoLocalGuardado;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 1. Si ya se registró el golpe en este turno, no hacemos nada
        if (golpeRegistrado) return;

        // 2. Reproducir sonido
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // 3. Capturar datos del impacto
        velocidadGuardada = collision.relativeVelocity.magnitude;
        Vector3 puntoImpactoGlobal = collision.contacts[0].point;
        puntoLocalGuardado = transform.InverseTransformPoint(puntoImpactoGlobal);

        // 4. ACTIVAR VFX (Si existe)
        if (vfxGolpe != null)
        {
            StartCoroutine(ManejarVFX(puntoImpactoGlobal));
        }

        Debug.Log($"[Detector] ¡IMPACTO! Vel: {velocidadGuardada:F1}");
        
        // 5. Cerramos el candado del turno
        golpeRegistrado = true;
    }

    // Lógica para mostrar y ocultar el efecto visual
    private IEnumerator ManejarVFX(Vector3 posicion)
    {
        // Movemos el VFX al punto exacto del golpe
        vfxGolpe.transform.position = posicion;
        
        // Lo activamos
        vfxGolpe.SetActive(true);

        // Esperamos el tiempo definido
        yield return new WaitForSeconds(duracionVFX);

        // Lo desactivamos
        vfxGolpe.SetActive(false);
    }

    public bool IntentarObtenerGolpe(out float velocidad, out Vector3 puntoLocal)
    {
        if (golpeRegistrado)
        {
            velocidad = velocidadGuardada;
            puntoLocal = puntoLocalGuardado;
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
        // Opcional: Nos aseguramos de que el VFX esté apagado al iniciar el turno
        if (vfxGolpe != null) vfxGolpe.SetActive(false);
    }
}