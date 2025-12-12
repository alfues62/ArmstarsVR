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
        // Si hay datos asignados, configuramos al enemigo al inicio
        if (datosBase != null)
        {
            ConfigurarEnemigo(datosBase);
        }
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
        Debug.Log($"{datosBase.nombreEnemigo} ataca causando {datosBase.ataque} de daño!");
        // Aquí conectaríamos con el jugador para quitarle vida
    }

    private void Morir()
    {
        Debug.Log($"{datosBase.nombreEnemigo} ha sido derrotado.");
        // Avisar al DueloManager de que ganamos
        DueloManager.Instance.CambiarEstado(DueloManager.Instance.VictoriaEstado);
    }
}