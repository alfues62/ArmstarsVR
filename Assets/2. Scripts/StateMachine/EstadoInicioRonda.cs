using UnityEngine;
using UnityEngine.UI;

public class EstadoInicioRonda : EstadoDuelo
{
    [Header("UI")]
    public Text textoRonda;
    public float tiempoCartel = 2.0f;

    private int contadorRonda = 0;

    public override void Entrar()
    {
        contadorRonda++;
        Debug.Log($"--- COMENZANDO RONDA {contadorRonda} ---");

        if (textoRonda != null)
        {
            textoRonda.gameObject.SetActive(true);
            textoRonda.text = "RONDA " + contadorRonda;
        }

        // Esperar y lanzar el turno
        Invoke("EmpezarCombate", tiempoCartel);
    }

    void EmpezarCombate()
    {
        if (textoRonda != null) textoRonda.gameObject.SetActive(false);

        // AQUÍ ESTÁ LA CLAVE:
        // No decidimos al azar ahora, sino que miramos qué se decidió al inicio.
        if (manager.jugadorEmpiezaLaRonda)
        {
            // Caso A: Jugador -> Enemigo
            manager.CambiarEstado(manager.estadoTurnoJugador);
        }
        else
        {
            // Caso B: Enemigo -> Jugador
            manager.CambiarEstado(manager.estadoTurnoEnemigo);
        }
    }
}