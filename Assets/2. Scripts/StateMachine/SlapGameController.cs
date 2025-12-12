using UnityEngine;
using System.Collections;

public class SlapGameController : MonoBehaviour
{
    // Definimos los estados posibles
    public enum GameState
    {
        WaitingToStart,
        PlayerTurn,
        EnemyTurn,
        ResolvingHit,
        ResetPositions,
        GameOver
    }

    [Header("Estado Actual")]
    public GameState currentState;

    [Header("Configuración")]
    public GameObject enemigo;
    public GameObject jugador;
    
    // Singleton simple para acceder fácil desde otros scripts
    public static SlapGameController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CambiarEstado(GameState.WaitingToStart);
    }

    // Función central para manejar cambios de estado
    public void CambiarEstado(GameState nuevoEstado)
    {
        currentState = nuevoEstado;
        Debug.Log("Estado cambiado a: " + currentState);

        switch (currentState)
        {
            case GameState.WaitingToStart:
                // Lógica de inicio (ej. cuenta atrás 3, 2, 1...)
                StartCoroutine(CuentaAtras());
                break;

            case GameState.PlayerTurn:
                Debug.Log("¡TU TURNO! Prepara la mano.");
                // Aquí podrías habilitar ayudas visuales en la mano del jugador
                break;

            case GameState.EnemyTurn:
                Debug.Log("TURNO DEL RIVAL. Prepárate.");
                // Aquí llamarías a la IA del enemigo para que inicie su animación de ataque
                EjecutarAtaqueEnemigo();
                break;

            case GameState.ResolvingHit:
                // Pausa breve o cámara lenta tras un golpe
                break;
                
            case GameState.ResetPositions:
                // Lógica para reiniciar ronda
                Invoke("ReiniciarRonda", 2f);
                break;
        }
    }

    // Ejemplo de corrutina para iniciar
    IEnumerator CuentaAtras()
    {
        yield return new WaitForSeconds(2);
        CambiarEstado(GameState.PlayerTurn); // Empiezas tú
    }

    void EjecutarAtaqueEnemigo()
    {
        // Aquí conectarías con tu script de IA
        // enemigo.GetComponent<EnemyAI>().Atacar();
    }
    
    void ReiniciarRonda()
    {
        // Volver a empezar turnos si nadie ha perdido
        CambiarEstado(GameState.PlayerTurn);
    }

    // Esta función la llamará tu script de colisión
    public void GolpeRegistrado(float velocidad, bool esGolpeDelJugador)
    {
        if (currentState == GameState.ResolvingHit) return; // Evitar doble golpe

        Debug.Log("Procesando daño con velocidad: " + velocidad);
        
        // Cambiamos a estado de resolución para bloquear más inputs momentáneamente
        CambiarEstado(GameState.ResolvingHit);

        // Aquí restarías vida
        // ...

        // Luego pasamos turno o reseteamos
        if (esGolpeDelJugador)
        {
            Invoke("TurnoEnemigo", 2f); // Espera 2 seg y cambio turno
        }
        else
        {
            Invoke("TurnoJugador", 2f);
        }
    }

    void TurnoEnemigo() => CambiarEstado(GameState.EnemyTurn);
    void TurnoJugador() => CambiarEstado(GameState.PlayerTurn);
}