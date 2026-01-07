using UnityEngine;

public class EstadoVictoria : EstadoDuelo // <--- IMPORTANTE: Heredar de EstadoDuelo
{
    [Header("Configuración UI")]
    public GameObject panelVictoria; // Arrastra tu Panel de "¡GANASTE!" aquí
    public float tiempoEnPantalla = 3.0f; // Tiempo modificable desde el Inspector

    public override void Entrar()
    {
        Debug.Log("--- ¡VICTORIA! ---");

        // 1. Mostrar la UI
        if (panelVictoria != null) panelVictoria.SetActive(true);

        // 2. Programar la salida
        Invoke("VolverAlInicio", tiempoEnPantalla);
    }

    void VolverAlInicio()
    {
        // 1. Ocultamos el cartel
        if (panelVictoria != null) panelVictoria.SetActive(false);

        // 2. Cerramos el combate
        // (Aquí llamamos a una función del Manager para limpiar la escena)
        manager.FinalizarCombate();
    }
}   