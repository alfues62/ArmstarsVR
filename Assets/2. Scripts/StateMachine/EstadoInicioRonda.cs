using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EstadoInicioRonda : EstadoDuelo
{
    [Header("UI")]
    public TextMeshProUGUI textoRonda;
    public float duracionCartel = 3.0f;

    private int contadorRonda = 0;

    public override void Entrar()
    {
        contadorRonda++;
        Debug.Log($"--- INICIO RONDA {contadorRonda} ---");

        if (textoRonda != null)
        {
            textoRonda.gameObject.SetActive(true);
            textoRonda.text = "RONDA " + contadorRonda;
        }

        Invoke("DistribuirTurnos", duracionCartel);
    }

    void DistribuirTurnos()
    {
        if (textoRonda != null) textoRonda.gameObject.SetActive(false);

        // Redirigimos según quién ganó el sorteo inicial
        if (manager.jugadorEmpiezaLaRonda)
            manager.CambiarEstado(manager.estadoTurnoJugador);
        else
            manager.CambiarEstado(manager.estadoTurnoEnemigo);
    }

    public override void Salir()
    {
        CancelInvoke();
    }
}