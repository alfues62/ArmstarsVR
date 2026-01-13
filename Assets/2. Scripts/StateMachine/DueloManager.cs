using UnityEngine;

public class DueloManager : MonoBehaviour
{
    public static DueloManager Instance;

    [Header("Referencias Principales")]
    public UnidadEnemiga enemigoActivo;
    public JugadorStats jugador;

    [Header("Configuración de Audio")]
    public AudioSource musicaCombate; // Arrastra aquí el AudioSource con la música

    [Header("Configuración de Flujo")]
    [HideInInspector] public bool jugadorEmpiezaLaRonda;

    [Header("Máquina de Estados")]
    public EstadoDuelo estadoInicioRonda;
    public EstadoDuelo estadoTurnoJugador;
    public EstadoDuelo estadoTurnoEnemigo;
    public EstadoDuelo GameOverEstado;
    public EstadoDuelo VictoriaEstado;

    private EstadoDuelo estadoActual;
    private bool dueloIniciado = false;

    public EstadoDuelo EstadoActual => estadoActual;

    public GameObject canvasDuelo;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        InicializarEstados();

        // Intentar obtener el componente si no se asignó manualmente
        if (musicaCombate == null) musicaCombate = GetComponent<AudioSource>();
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
        InicializarEstados();

        IniciarDuelo();
    }

    private void IniciarDuelo()
    {
        dueloIniciado = true;

        // --- INICIAR MÚSICA ---
        if (musicaCombate != null)
        {
            musicaCombate.Play();
        }

        jugadorEmpiezaLaRonda = (Random.value > 0.5f);
        Debug.Log(jugadorEmpiezaLaRonda ? ">>> Sorteo: Empieza JUGADOR" : ">>> Sorteo: Empieza ENEMIGO");

        CambiarEstado(estadoInicioRonda);

        canvasDuelo.SetActive(true);
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

        // --- DETENER MÚSICA ---
        if (musicaCombate != null)
        {
            musicaCombate.Stop();
        }

        if (enemigoActivo != null) enemigoActivo.gameObject.SetActive(false);
        canvasDuelo.SetActive(true);
        Debug.Log("--- Combate Finalizado ---");


    }
}