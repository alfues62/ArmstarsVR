using UnityEngine;

public class DueloManager : MonoBehaviour
{
    public static DueloManager Instance;

    [Header("Referencias Principales")]
    public UnidadEnemiga enemigoActivo;
    public JugadorStats jugador;

    [Header("Configuración de Flujo")]
    // Oculto para evitar manipulación manual, pero público para acceso lógico
    [HideInInspector] public bool jugadorEmpiezaLaRonda;

    [Header("Máquina de Estados")]
    public EstadoDuelo estadoInicioRonda;
    public EstadoDuelo estadoTurnoJugador;
    public EstadoDuelo estadoTurnoEnemigo;
    public EstadoDuelo GameOverEstado;
    public EstadoDuelo VictoriaEstado;

    private EstadoDuelo estadoActual;
    private bool dueloIniciado = false;

    // Propiedad pública de solo lectura
    public EstadoDuelo EstadoActual => estadoActual;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        InicializarEstados();
    }

    void Update()
    {
        if (!dueloIniciado || estadoActual == null) return;
        estadoActual.Actualizar();
    }

    private void InicializarEstados()
    {
        if (estadoInicioRonda) estadoInicioRonda.Inicializar(this);
        if (estadoTurnoJugador) estadoTurnoJugador.Inicializar(this);
        if (estadoTurnoEnemigo) estadoTurnoEnemigo.Inicializar(this);
        if (GameOverEstado) GameOverEstado.Inicializar(this);
        if (VictoriaEstado) VictoriaEstado.Inicializar(this);
    }

    public void PrepararYComenzarDuelo(UnidadEnemiga cuerpoFisico, DatosEnemigo fichaDatos)
    {
        enemigoActivo = cuerpoFisico;
        jugador = JugadorStats.Instance;

        jugador.PrepararParaCombate();
        InicializarEstados(); // Reiniciamos referencias en los estados

        IniciarDuelo();
    }

    private void IniciarDuelo()
    {
        dueloIniciado = true;

        // Sorteo 50/50
        jugadorEmpiezaLaRonda = (Random.value > 0.5f);
        Debug.Log(jugadorEmpiezaLaRonda ? ">>> Sorteo: Empieza JUGADOR" : ">>> Sorteo: Empieza ENEMIGO");

        CambiarEstado(estadoInicioRonda);
    }

    public void CambiarEstado(EstadoDuelo nuevoEstado)
    {
        if (estadoActual != null) estadoActual.Salir();
        estadoActual = nuevoEstado;
        if (estadoActual != null) estadoActual.Entrar();
    }

    public void FinalizarCombate()
    {
        dueloIniciado = false;
        if (enemigoActivo != null) enemigoActivo.gameObject.SetActive(false);
        Debug.Log("--- Combate Finalizado ---");
    }
}