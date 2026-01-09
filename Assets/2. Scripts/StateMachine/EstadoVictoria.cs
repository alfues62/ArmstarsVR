using UnityEngine;

public class EstadoVictoria : EstadoDuelo
{
    public GameObject panelVictoria;
    public float tiempoEnPantalla = 3.0f;

    public override void Entrar()
    {
        Debug.Log("--- ¡VICTORIA! ---");
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Invoke("Cerrar", tiempoEnPantalla);
    }

    void Cerrar()
    {
        if (panelVictoria != null) panelVictoria.SetActive(false);
        manager.FinalizarCombate();
    }
}