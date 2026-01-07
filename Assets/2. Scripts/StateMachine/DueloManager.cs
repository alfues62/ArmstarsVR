using UnityEngine;
public class DueloManager : MonoBehaviour
{
    public static DueloManager Instance;

    [Header("Referencias de Combate")]
    public UnidadEnemiga enemigoActivo;
    public JugadorStats jugador;
    [HideInInspector] public bool jugadorEmpiezaLaRonda;

    [Header("Los Cassettes (Estados)")]
    public EstadoDuelo estadoInicioRonda;
    public EstadoDuelo estadoTurnoJugador;
    public EstadoDuelo estadoTurnoEnemigo;
    public EstadoDuelo GameOverEstado;
    public EstadoDuelo VictoriaEstado;

    private EstadoDuelo estadoActual;

    public EstadoDuelo getEstadoActual()
    {
        return estadoActual;
    }

    private bool dueloIniciado = false;

    void Awake()
    {
        // Inicializamos todos los estados disponibles
        if (estadoInicioRonda) estadoInicioRonda.Inicializar(this);
        estadoTurnoJugador.Inicializar(this);
        estadoTurnoEnemigo.Inicializar(this);
        if (GameOverEstado) GameOverEstado.Inicializar(this);
        if (VictoriaEstado) VictoriaEstado.Inicializar(this);
    }

    void Update()
    {
        if (!dueloIniciado) return;
        if (estadoActual != null) estadoActual.Actualizar();
    }
    public void PrepararYComenzarDuelo(UnidadEnemiga cuerpoFisico, DatosEnemigo fichaDatos)
    {
        // 1. INYECTAMOS LA FICHA EN EL CUERPO
        cuerpoFisico.ConfigurarEnemigo(fichaDatos);

        // 2. CONECTAMOS LOS CABLES AL MANAGER
        enemigoActivo = cuerpoFisico;
        jugador = JugadorStats.Instance; // Aseguramos que el jugador está conectado
        jugador.PrepararParaCombate();
        // 3. AHORA SÍ, INICIALIZAMOS LOS ESTADOS
        // (Ahora es seguro porque 'enemigoActivo' ya no es null)
        if (estadoInicioRonda) estadoInicioRonda.Inicializar(this);
        estadoTurnoJugador.Inicializar(this);
        estadoTurnoEnemigo.Inicializar(this);
        if (GameOverEstado) GameOverEstado.Inicializar(this);
        if (VictoriaEstado) VictoriaEstado.Inicializar(this);

        // 4. ¡EMPEZAR!
        IniciarDuelo();
    }
    public void IniciarDuelo()
    {
        dueloIniciado = true;

        // --- AQUÍ LANZAMOS LA MONEDA UNA SOLA VEZ ---
        // Si sale más de 0.5, empieza el jugador. Si no, el enemigo.
        jugadorEmpiezaLaRonda = (Random.value > 0.5f);

        if (jugadorEmpiezaLaRonda)
            Debug.Log("Sorteo inicial: Empieza el JUGADOR.");
        else
            Debug.Log("Sorteo inicial: Empieza el ENEMIGO.");

        // Vamos directos a la Ronda 1
        CambiarEstado(estadoInicioRonda);
    }
    public void FinalizarCombate()
    {
        dueloIniciado = false;

        // 1. Ocultar al enemigo
        if (enemigoActivo != null)
        {
            enemigoActivo.gameObject.SetActive(false);
        }

        Debug.Log("Combate finalizado. Volviendo al modo exploración/menú.");

        // Aquí podrías reactivar tu menú principal si lo tenías oculto
        // Ejemplo: menuPrincipal.SetActive(true);
    }
    public void CambiarEstado(EstadoDuelo nuevoEstado)
    {
        if (estadoActual != null) estadoActual.Salir();
        estadoActual = nuevoEstado;
        if (estadoActual != null) estadoActual.Entrar();
    }
}