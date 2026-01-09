using UnityEngine;

public class EstadoTurnoEnemigo : EstadoDuelo
{
    [Header("Configuración")]
    public float esperaAntesDeAtacar = 1.5f;

    public override void Entrar()
    {
        Debug.Log("TURNO ENEMIGO: Preparando ataque...");
        Invoke("EjecutarAtaque", esperaAntesDeAtacar);
    }

    private void EjecutarAtaque()
    {
        // Verificamos que sigamos en este estado (por si el juego se pausó o terminó)
        if (manager.EstadoActual != this) return;

        manager.enemigoActivo.Atacar();

        // Si el jugador sigue vivo, gestionamos el pase de turno
        if (manager.EstadoActual == this)
        {
            TerminarTurno();
        }
    }

    void TerminarTurno()
    {
        if (manager.jugadorEmpiezaLaRonda)
        {
            // Jugador 1º, Enemigo 2º -> Fin de Ronda
            manager.CambiarEstado(manager.estadoInicioRonda);
        }
        else
        {
            // Enemigo 1º -> Turno Jugador
            manager.CambiarEstado(manager.estadoTurnoJugador);
        }
    }

    public override void Salir()
    {
        CancelInvoke(); // Seguridad
    }
}