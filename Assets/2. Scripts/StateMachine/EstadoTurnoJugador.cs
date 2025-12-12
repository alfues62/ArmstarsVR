using UnityEngine;

public class EstadoTurnoJugador : EstadoDuelo
{
    public override void Entrar()
    {
        Debug.Log("--- INICIO TURNO JUGADOR ---");
        Debug.Log("Habilitando UI de ataques...");
        // Aquí habilitarías los botones de ataque
    }

    public override void Actualizar()
    {
        // Lógica de espera: el jugador elige una acción.
        // Ejemplo rápido para probar: Si pulsa espacio, gana.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jugador atacó y ganó.");
            // Cambiamos al estado de victoria
            manager.CambiarEstado(manager.VictoriaEstado);
        }
    }

    public override void Salir()
    {
        Debug.Log("Deshabilitando UI de ataques...");
    }
}