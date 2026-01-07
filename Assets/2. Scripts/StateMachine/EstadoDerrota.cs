using UnityEngine;

public class EstadoDerrota : EstadoDuelo // <--- IMPORTANTE: Heredar de EstadoDuelo
{
    [Header("Configuración UI")]
    public GameObject panelDerrota; // Arrastra tu Panel de "GAME OVER" aquí
    public float tiempoEnPantalla = 4.0f; // Quizás quieras dejar este cartel más tiempo

    public override void Entrar()
    {
        Debug.Log("--- DERROTA ---");

        // 1. Mostrar la UI
        if (panelDerrota != null) panelDerrota.SetActive(true);

        // 2. Programar la salida
        Invoke("VolverAlInicio", tiempoEnPantalla);
    }

    void VolverAlInicio()
    {
        if (panelDerrota != null) panelDerrota.SetActive(false);

        // Cerramos el combate igual que en la victoria
        manager.FinalizarCombate();
    }
}