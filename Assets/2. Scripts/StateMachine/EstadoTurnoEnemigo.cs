using UnityEngine;

public class EstadoTurnoEnemigo : EstadoDuelo
{
    public override void Entrar()
    {
        Debug.Log(">>> Iniciando Turno del Enemigo...");
        Invoke("EjecutarAccion", 1.5f);
    }

    private void EjecutarAccion()
    {
        // 1. El ataque
        manager.enemigoActivo.Atacar();

        // 2. Seguridad: Si el juego terminó (GameOver/Victoria), paramos aquí.
        if (manager.getEstadoActual() != manager.estadoTurnoEnemigo) return;

        // 3. DECISIÓN DE TRÁFICO
        // ¿Quién empezó esta ronda?
        if (manager.jugadorEmpiezaLaRonda)
        {
            // CASO A: El orden fue Jugador -> Enemigo.
            // Como yo (Enemigo) soy el segundo, la ronda ha terminado.
            Debug.Log("Fin de la ronda. Volviendo al inicio.");
            manager.CambiarEstado(manager.estadoInicioRonda);
        }
        else
        {
            // CASO B: El orden fue Enemigo -> Jugador.
            // Como yo (Enemigo) fui el primero, ahora le toca al Jugador.
            Debug.Log("Paso el turno al Jugador.");
            manager.CambiarEstado(manager.estadoTurnoJugador);
        }
    }
}

