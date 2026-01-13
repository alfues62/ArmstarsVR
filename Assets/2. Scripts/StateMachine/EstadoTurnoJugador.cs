using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EstadoTurnoJugador : EstadoDuelo
{
    [Header("Referencias Físicas")]
    public GameObject enemigoColisionador;   // El objeto físico del enemigo
    public DetectorDeGolpes detectorEnemigo; // El script del sensor

    [Header("Configuración")]
    public Vector3 posicionGuardia = new Vector3(0.23f, 0.34f, 0.25f);
    public Vector3 rotacionGuardia = new Vector3(90f, 41f, 0f);

    public TextMeshProUGUI textoTurno;

    private bool procesandoGolpe = false;

    public override void Entrar()
    {
        procesandoGolpe = false;

        // 1. Preparamos el detector
        if (detectorEnemigo != null) detectorEnemigo.PrepararNuevoTurno();

        // 2. Reseteamos físicas del enemigo (evitar que se mueva solo)
        if (enemigoColisionador != null)
        {
            Rigidbody rb = enemigoColisionador.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            // Colocamos al enemigo en posición para recibir el golpe
            enemigoColisionador.transform.localPosition = posicionGuardia;
            enemigoColisionador.transform.localEulerAngles = rotacionGuardia;
        }

        textoTurno.gameObject.SetActive(true);
        textoTurno.text = "Tu turno, ¡Golpea!";

        Debug.Log("TURNO JUGADOR: ¡Tienes 1 golpe!");
    }

    public override void Actualizar()
    {
        // Si ya golpeamos, no hacemos nada más hasta que cambie el estado
        if (procesandoGolpe) return;

        if (detectorEnemigo != null && detectorEnemigo.golpeRegistrado)
        {
            if (detectorEnemigo.IntentarObtenerGolpe(out float velocidad, out Vector3 posicion))
            {
                // CRÍTICO: Bloqueamos inmediatamente para asegurar que sea SOLO UN HIT
                procesandoGolpe = true;

                CongelarEnemigo(); // Opcional: feedback visual
                ProcesarGolpe(velocidad);
            }
        }
    }

    void CongelarEnemigo()
    {
        Rigidbody rb = enemigoColisionador.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void ProcesarGolpe(float velocidad)
    {
        // Cálculo de daño
        int dañoBase = JugadorStats.Instance.ataqueBase;
        int dañoTotal = Mathf.RoundToInt(velocidad * 2.0f) + dañoBase;

        // Aplicar daño
        manager.enemigoActivo.RecibirDaño(dañoTotal);

        // Si el enemigo no murió (el estado sigue siendo este), pasamos turno.
        if (manager.EstadoActual == this)
        {
            Invoke("TerminarTurno", 0.5f); // Pequeño delay para ver el impacto
        }
    }

    void TerminarTurno()
    {
        // Lógica de "Ping Pong"
        if (manager.jugadorEmpiezaLaRonda)
        {
            // Jugador fue 1º -> Ahora va el Enemigo
            manager.CambiarEstado(manager.estadoTurnoEnemigo);
        }
        else
        {
            // Jugador fue 2º -> Se acabó la ronda, volvemos al inicio
            manager.CambiarEstado(manager.estadoInicioRonda);
        }
    }
}