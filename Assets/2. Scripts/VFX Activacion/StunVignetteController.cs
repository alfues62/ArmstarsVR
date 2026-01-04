using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class StunVignetteController : MonoBehaviour
{
    public Volume globalVolume;
    private Vignette vignette;

    [Header("Configuración de Impacto")]
    public float intensidadMaxima = 0.5f;
    public float duracionDesvanecimiento = 1.5f;

    void Start()
    {
        // Buscamos el efecto de Vignette dentro del perfil del Volume
        if (globalVolume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    // Llama a esta función cuando el jugador reciba un golpe
    public void DispararVignette()
    {
        StopAllCoroutines();
        StartCoroutine(EfectoStun());
    }

    private IEnumerator EfectoStun()
    {
        float tiempo = 0;

        // Fase de impacto: el vignette aparece instantáneamente o muy rápido
        vignette.intensity.value = intensidadMaxima;

        // Fase de recuperación: vuelve a 0 gradualmente para simular recuperación
        while (tiempo < duracionDesvanecimiento)
        {
            tiempo += Time.deltaTime;
            // Usamos un lerp para suavizar la desaparición
            vignette.intensity.value = Mathf.Lerp(intensidadMaxima, 0, tiempo / duracionDesvanecimiento);
            yield return null;
        }

        vignette.intensity.value = 0f;
    }
}