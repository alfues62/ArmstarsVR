using UnityEngine;

public class EstadoTurnoEnemigo : EstadoDuelo
{
    [Header("Configuración")]
    public float esperaAntesDeAnimacion = 1.5f;

    public override void Entrar()
    {
        Debug.Log("TURNO ENEMIGO: Pensando...");
        // 1. Esperamos el tiempo de "tensión" antes de movernos
        Invoke("OrdenDeAtacar", esperaAntesDeAnimacion);
    }

    private void OrdenDeAtacar()
    {
        // Seguridad: Si el juego se detuvo o cambió de estado, no hacemos nada
        if (manager.EstadoActual != this) return;

        Debug.Log("TURNO ENEMIGO: ¡Iniciando animación!");

        // 2. Aquí SOLO activamos la animación. NO aplicamos daño todavía.
        // Llamamos al método nuevo en UnidadEnemiga
        manager.enemigoActivo.IniciarAnimacionAtaque();
    }

    public void ConfirmarDañoYTerminar()
    {
        Debug.Log("TURNO ENEMIGO: Golpe conectado. Finalizando turno.");

        // Lógica de "Ping Pong" de turnos
        if (manager.jugadorEmpiezaLaRonda)
        {
            // Jugador fue 1º -> Fin de Ronda
            manager.CambiarEstado(manager.estadoInicioRonda);
        }
        else
        {
            // Enemigo fue 1º -> Turno Jugador
            manager.CambiarEstado(manager.estadoTurnoJugador);
        }
    }

    public override void Salir()
    {
        CancelInvoke();
    }
}