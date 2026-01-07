using UnityEngine;
using UnityEngine.UI; // Si vas a mostrar la vida en una barra

public class UnidadEnemiga : MonoBehaviour
{
    [Header("Arrastra aquí el archivo del enemigo")]
    public DatosEnemigo datosBase; // Aquí arrastras tu "Goblin" o "Dragon"

    [Header("Referencias Visuales")]
    public Image imagenRenderer; // El componente Image de la UI o SpriteRenderer

    // Variables de estado "En vivo" (Runtime)
    private int vidaActual;

    void Start()
    {
    }

    // Método para "Inyectar" un enemigo en este cuerpo
    public void ConfigurarEnemigo(DatosEnemigo nuevosDatos)
    {
        datosBase = nuevosDatos;

        // IMPORTANTE: Copiamos la vidaMax a una variable local.
        // Nunca modifiques 'datosBase.vidaMax' directamente en combate,
        // o el archivo se quedará guardado con la vida bajada para siempre.
        vidaActual = datosBase.vidaMax;

        // Cambiamos el gráfico
        if (imagenRenderer != null && datosBase.spriteEnemigo != null)
        {
            imagenRenderer.sprite = datosBase.spriteEnemigo;
        }

        Debug.Log($"Ha aparecido un {datosBase.nombreEnemigo} con {vidaActual} HP.");
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"{datosBase.nombreEnemigo} recibe {cantidad} de daño. Vida restante: {vidaActual}");

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Atacar()
    {
        if (JugadorStats.Instance == null) return;

        // 1. CÁLCULO DE LA PRECISIÓN PONDERADA
        float azar1 = Random.Range(-datosBase.variabilidad, datosBase.variabilidad);
        float azar2 = Random.Range(-datosBase.variabilidad, datosBase.variabilidad);
        float variacionReal = (azar1 + azar2) / 2f;

        float precisionFinal = datosBase.precisionBase + variacionReal;
        precisionFinal = Mathf.Clamp(precisionFinal, 0f, 1f);

        // 2. CÁLCULO DEL DAÑO BRUTO (Float)
        float dañoBrutoFloat = datosBase.ataque * precisionFinal;

        // 3. CONVERSIÓN A INT (Redondeo)
        // Necesitamos pasar un entero al jugador. RoundToInt redondea al más cercano.
        int dañoEnvio = Mathf.RoundToInt(dañoBrutoFloat);

        // 4. EJECUTAR
        // Nota: El enemigo NO sabe cuánto se reducirá por defensa, así que solo logueamos lo que él envía.
        Debug.Log($"{datosBase.nombreEnemigo} ataca con precisión del {(precisionFinal * 100):F1}% y fuerza bruta de {dañoEnvio}.");

        // CORRECCIÓN: Enviamos 'dañoEnvio' (que es el bruto), NO 'dañoFinal'
        JugadorStats.Instance.RecibirDaño(dañoEnvio);
    }

    private void Morir()
    {
        Debug.Log($"{datosBase.nombreEnemigo} ha sido derrotado.");
        // Avisar al DueloManager de que ganamos
        DueloManager.Instance.CambiarEstado(DueloManager.Instance.VictoriaEstado);
    }
}