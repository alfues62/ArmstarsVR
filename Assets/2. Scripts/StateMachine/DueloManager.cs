using UnityEngine;

public class DueloManager : MonoBehaviour
{
    public static DueloManager Instance; // Singleton

    [Header("Los Cassettes (Estados)")]
    public EstadoDuelo estadoTurnoJugador;
    public EstadoDuelo estadoTurnoEnemigo;
    public EstadoDuelo GameOverEstado;
    public EstadoDuelo VictoriaEstado;

    private EstadoDuelo estadoActual;
    private bool dueloIniciado = false;

    void Awake()
    {
        Instance = this;
        // Inicializamos los estados (les decimos quién es el jefe)
        estadoTurnoJugador.Inicializar(this);
        estadoTurnoEnemigo.Inicializar(this);
    }

    void Update()
    {
        // Si no han dado la orden de empezar, no hacemos NADA.
        if (!dueloIniciado) return;

        if (estadoActual != null)
        {
            estadoActual.Actualizar();
        }
    }

    // --- ESTA ES LA LLAVE DE ARRANQUE ---
    // Esta función la llamará el NPC, una cinemática, o un botón.
    public void IniciarDuelo()
    {
        Debug.Log("¡X ha activado el duelo!");
        dueloIniciado = true;
        
        // Ponemos el primer estado (ej. Turno del Jugador)
        CambiarEstado(estadoTurnoJugador);
    }

    public void CambiarEstado(EstadoDuelo nuevoEstado)
    {
        if (estadoActual != null) estadoActual.Salir();
        estadoActual = nuevoEstado;
        if (estadoActual != null) estadoActual.Entrar();
    }
}