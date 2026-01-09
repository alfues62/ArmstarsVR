using UnityEngine;

public class EstadoDerrota : EstadoDuelo
{
    public GameObject panelDerrota;
    public float tiempoEnPantalla = 3.0f;

    public override void Entrar()
    {
        Debug.Log("--- Derrota ---");
        if (panelDerrota != null) panelDerrota.SetActive(true);
        Invoke("Cerrar", tiempoEnPantalla);
    }

    void Cerrar()
    {
        if (panelDerrota != null) panelDerrota.SetActive(false);
        manager.FinalizarCombate();
    }
}