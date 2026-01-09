using UnityEngine;

public class EstadoTurnoJugador : EstadoDuelo
{
    public DetectorDeGolpes detectorEnemigo;
    public override void Entrar()
    {
        Debug.Log("--- TU TURNO: ¡Golpea! ---");

        detectorEnemigo.PrepararNuevoTurno();
    }

    public override void Actualizar()
    {
        // Preguntamos constantemente: "¿Ya te han pegado?"
        if (detectorEnemigo != null)
        {
            // Creamos variables vacías para recibir los datos
            float vel;
            Vector3 pos;

            // Si IntentarObtenerGolpe devuelve TRUE, es que ya tenemos el primer valor
            if (detectorEnemigo.IntentarObtenerGolpe(out vel, out pos))
            {
                ProcesarAtaque(vel, pos);
            }
        }
    }
    void ProcesarAtaque(float velocidad, Vector3 posicion)
    {
        Debug.Log($"¡GOLPE PROCESADO! Vel: {velocidad} | Pos: {posicion}");

        // ... Aquí va tu lógica de daño ...

        // Importante: Cambiamos de estado para dejar de preguntar
        manager.CambiarEstado(manager.estadoTurnoEnemigo);
    }
    void RealizarAtaque()
    {
        // 1. CALCULAMOS Y APLICAMOS DAÑO
        // Obtenemos cuánto pega el jugador
        int daño = JugadorStats.Instance.ataqueBase;

        Debug.Log($"Jugador ataca causando {daño} puntos.");

        // Se lo aplicamos al enemigo actual
        manager.enemigoActivo.RecibirDaño(daño);


        // 2. SEGURIDAD (Critical Check)
        // Si el enemigo muere, 'RecibirDaño' cambiará el estado a 'VictoriaEstado'.
        // Si eso pasa, NO debemos intentar cambiar de turno aquí.
        if (manager.getEstadoActual() != manager.estadoTurnoJugador) return;


        // 3. DECISIÓN DE TRÁFICO (Lógica Espejo)
        // Consultamos la variable que definimos en el Manager
        if (manager.jugadorEmpiezaLaRonda)
        {
            // CASO A: El Jugador fue PRIMERO.
            // Todavía falta que pegue el enemigo.
            Debug.Log("Fin de mi turno -> Va el Enemigo.");
            manager.CambiarEstado(manager.estadoTurnoEnemigo);
        }
        else
        {
            // CASO B: El Jugador fue SEGUNDO (El enemigo ya pegó antes).
            // La ronda ha terminado.
            Debug.Log("Fin de la ronda -> Volvemos al inicio.");
            manager.CambiarEstado(manager.estadoInicioRonda);
        }
    }

    public override void Salir()
    {
        // Aquí deshabilitarías los botones para que no se pueda pulsar dos veces
        Debug.Log("Terminando turno jugador...");
    }
}