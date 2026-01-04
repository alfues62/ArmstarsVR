using UnityEngine;
using UnityEngine.VFX; // Necesario para VFX Graph

public class StunEffectController : MonoBehaviour // El VFX deberia estar puesto en el enemigo
{

    // ESTO ES POR SI SIRVE A LA HORA DE IMPLEMENTAR LAS COSAS
    //
    // Referencia al controlador
    //StunEffectController stunController = GetComponent<StunEffectController>();
    //
    // Aplicar stun de 2.5 segundos
    //stunController.AplicarStunTemporal(2.5f);
    //


    [Tooltip("Arrastra aquí el Particle System de los pajaritos")]
    [SerializeField] private ParticleSystem stunParticles;

    // Método para activar el efecto
    public void ActivarStun()
    {
        if (stunParticles != null && !stunParticles.isPlaying)
        {
            stunParticles.Play();
        }
    }

    // Método para desactivar el efecto
    public void DesactivarStun()
    {
        if (stunParticles != null)
        {
            // StopEmittingAndClear elimina los pajaritos al instante.
            // Si prefieres que desaparezcan poco a poco, usa solo stunParticles.Stop();
            stunParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    // --- EJEMPLOS DE USO ---

    // 1. Ejemplo para probarlo rápido con el teclado (Tecla K)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (stunParticles.isPlaying) DesactivarStun();
            else ActivarStun();
        }
    }

    // 2. Ejemplo para activarlo por un tiempo determinado (3 segundos)
    public void AplicarStunTemporal(float duracion)
    {
        ActivarStun();
        Invoke(nameof(DesactivarStun), duracion);
    }
}