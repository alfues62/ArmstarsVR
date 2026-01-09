using UnityEngine;

public class UnidadEnemiga : MonoBehaviour
{
    [Header("Referencias Visuales")]
    public Animator animator;   // Arrastra aquí el Animator

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
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"{gameObject.name} recibió {cantidad} de daño. Vida restante: {vidaActual}");

        if (vidaActual <= 0)
        {
            //Si quieres aqui va la animacion de muerte o las estrellas como veas es cuando muere el enemigo
            DueloManager.Instance.CambiarEstado(DueloManager.Instance.VictoriaEstado);
        }
        else
        {
            // Feedback visual de dolor
            if (animator != null) animator.SetTrigger("Hit");
        }
    }

    public void IniciarAnimacionAtaque()
    {
        if (animator != null)
        {
            //Esto llama a la animacion de ataque del enemigo.
            // El daño se calculará cuando la animación llegue al frame del impacto.
            animator.SetTrigger("Atacar");
        }
        else
        {
            Debug.LogWarning("No hay Animator asignado. Calculando daño directo.");
            GolpeEnemigo();
        }
    }

    // --- ATAQUE FASE 2: IMPACTO (Llamado por Animation Event) ---
    // ESTA es la función que debes poner en el evento de la animación
    public void GolpeEnemigo()
    {
        if (JugadorStats.Instance == null || datos == null) return;


        // 1. CÁLCULO DE LA PRECISIÓN (Curva de campana / Promedio de dos randoms)
        float azar1 = Random.Range(-datos.variabilidad, datos.variabilidad);
        float azar2 = Random.Range(-datos.variabilidad, datos.variabilidad);
        float variacionReal = (azar1 + azar2) / 2f;

        float precisionFinal = Mathf.Clamp(datos.precisionBase + variacionReal, 0f, 1f);

        // 2. CÁLCULO DEL DAÑO FINAL
        int dañoFinal = Mathf.RoundToInt(datos.ataque * precisionFinal);

        Debug.Log($"[Enemigo] Impacto calculado: Daño {dañoFinal} (Precisión: {precisionFinal:P0})");

        // 3. APLICAR DAÑO AL JUGADOR
        JugadorStats.Instance.RecibirDaño(dañoFinal);

        // 4. AVISAR AL MANAGER (Fin de turno)
        if (DueloManager.Instance.EstadoActual is EstadoTurnoEnemigo estado)
        {
            estado.ConfirmarDañoYTerminar();
        }
    }
}