using UnityEngine;

public class EstadoVictoria : EstadoDuelo
{
    public GameObject panelVictoria;
    public float tiempoEnPantalla = 4.0f;

    public EnemyBeaten ragdollscript;

    public override void Entrar()
    {
        Debug.Log("--- ¡VICTORIA! ---");
        ragdollscript.BeatEnemy();
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Invoke("Cerrar", tiempoEnPantalla);
    }

    void Cerrar()
    {
        if (panelVictoria != null) panelVictoria.SetActive(false);
        manager.FinalizarCombate();
    }
}