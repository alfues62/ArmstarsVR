using UnityEngine;

public class UnidadEnemiga : MonoBehaviour
{
    [Header("Referencias Visuales")]
    public Animator animator;

    [Header("Configuración de Audio")]
    public AudioSource audioSource;
    public float delaySonido = 0.3f;

    [Header("Datos y Estado")]
    public DatosEnemigo datos;  // La ficha de datos (ScriptableObject)
    public int vidaActual;

    // Inicialización llamada por Activador o Manager
    public void ConfigurarEnemigo(DatosEnemigo nuevosDatos)
    {
        datos = nuevosDatos;
        if (datos != null)
        {
            vidaActual = datos.vidaMax;
            Debug.Log($"[Enemigo] Aparece {datos.nombreEnemigo} ({vidaActual} HP).");
        }

        // Si no se asignó por inspector, buscamos en el objeto
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"{gameObject.name} recibió {cantidad} de daño. Vida restante: {vidaActual}");

        if (vidaActual <= 0)
        {
            DueloManager.Instance.CambiarEstado(DueloManager.Instance.VictoriaEstado);
        }
        else
        {
            if (animator != null) animator.SetTrigger("Hit");
        }
    }

    public void IniciarAnimacionAtaque()
    {
        if (animator != null)
        {
            animator.SetTrigger("slapAnimTrigger");

            // --- LÓGICA DE AUDIO CON DELAY ---
            Invoke("ReproducirSonido", delaySonido);
            // ---------------------------------

            // Nota: En tu script original llamabas a GolpeEnemigo() aquí directamente.
            // Si usas Animation Events, deberías quitar esta línea para que no golpee dos veces.
            GolpeEnemigo();
        }
        else
        {
            Debug.LogWarning("No hay Animator asignado. Calculando daño directo.");
            ReproducirSonido(); // Sonido inmediato si no hay animación
            GolpeEnemigo();
        }
    }

    private void ReproducirSonido()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    // --- ATAQUE FASE 2: IMPACTO ---
    public void GolpeEnemigo()
    {
        if (JugadorStats.Instance == null || datos == null) return;

        float azar1 = Random.Range(-datos.variabilidad, datos.variabilidad);
        float azar2 = Random.Range(-datos.variabilidad, datos.variabilidad);
        float variacionReal = (azar1 + azar2) / 2f;

        float precisionFinal = Mathf.Clamp(datos.precisionBase + variacionReal, 0f, 1f);
        int dañoFinal = Mathf.RoundToInt(datos.ataque * precisionFinal);

        Debug.Log($"[Enemigo] Impacto calculado: Daño {dañoFinal} (Precisión: {precisionFinal:P0})");

        JugadorStats.Instance.RecibirDaño(dañoFinal);

        if (DueloManager.Instance.EstadoActual is EstadoTurnoEnemigo estado)
        {
            estado.ConfirmarDañoYTerminar();
        }
    }
}