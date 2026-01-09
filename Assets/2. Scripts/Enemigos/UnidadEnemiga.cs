using UnityEngine;
using UnityEngine.UI;

public class UnidadEnemiga : MonoBehaviour
{
    [Header("Datos Base")]
    public DatosEnemigo datosBase;

    [Header("Referencias Visuales")]
    public Image imagenRenderer; // O SpriteRenderer si es 3D puro

    // Variables de estado "En vivo"
    private int vidaActual;

    // Método para "Inyectar" un enemigo en este cuerpo
    public void ConfigurarEnemigo(DatosEnemigo nuevosDatos)
    {
        datosBase = nuevosDatos;

        // Copiamos la vidaMax a una variable local para no dañar el ScriptableObject
        vidaActual = datosBase.vidaMax;

        // Actualizamos el gráfico
        if (imagenRenderer != null && datosBase.spriteEnemigo != null)
        {
            imagenRenderer.sprite = datosBase.spriteEnemigo;
        }

        Debug.Log($"[Enemigo] Aparece {datosBase.nombreEnemigo} ({vidaActual} HP).");
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"[Enemigo] Recibe {cantidad} daño. Vida: {vidaActual}");

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Atacar()
    {
        if (JugadorStats.Instance == null) return;

        // 1. CÁLCULO DE LA PRECISIÓN (Variación aleatoria)
        float azar1 = Random.Range(-datosBase.variabilidad, datosBase.variabilidad);
        float azar2 = Random.Range(-datosBase.variabilidad, datosBase.variabilidad);
        float variacionReal = (azar1 + azar2) / 2f; // Promedio para curva de campana suave

        float precisionFinal = Mathf.Clamp(datosBase.precisionBase + variacionReal, 0f, 1f);

        // 2. CÁLCULO DEL DAÑO
        int dañoFinal = Mathf.RoundToInt(datosBase.ataque * precisionFinal);

        // 3. EJECUTAR
        Debug.Log($"[Enemigo] Ataca con fuerza {dañoFinal} (Precisión: {precisionFinal:P0})");
        JugadorStats.Instance.RecibirDaño(dañoFinal);
    }

    private void Morir()
    {
        Debug.Log($"[Enemigo] {datosBase.nombreEnemigo} derrotado.");
        // Avisamos al Manager DIRECTAMENTE para cambiar de estado
        DueloManager.Instance.CambiarEstado(DueloManager.Instance.VictoriaEstado);
    }
}