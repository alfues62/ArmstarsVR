using UnityEngine;

public class JugadorStats : MonoBehaviour
{
    // Singleton simple: Para que los enemigos sepan a quién atacar fácilmente
    public static JugadorStats Instance;

    [Header("Configuración Base")]
    public int vidaMaxima = 100;
    public int ataqueBase = 15;
    
    // Estas son las variables que cambian durante la pelea
    [Header("Estado Actual (Read Only)")]
    [SerializeField] private int vidaActual; // SerializeField para verla en inspector pero protegerla

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Al empezar la batalla, nos ponemos a tope de vida
        // (O aquí cargaríamos la vida que traías del mapa si fuera un RPG largo)
        vidaActual = vidaMaxima;
        
        // Actualizar la barra de vida de la UI aquí...
    }

    // --- MÉTODOS DE ACCIÓN ---

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"¡Auch! Jugador recibe {cantidad} daño. Vida: {vidaActual}");
        
        // Actualizar UI...

        if (vidaActual <= 0)
        {
            Derrota();
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima) vidaActual = vidaMaxima;
        
        Debug.Log($"Jugador se cura. Vida: {vidaActual}");
        // Actualizar UI...
    }

    // Método para que el Manager o el Estado obtengan el daño del jugador
    public int ObtenerDañoAtaque()
    {
        // Aquí podrías sumar modificadores (espada equipada, buffs, etc.)
        return ataqueBase;
    }

    private void Derrota()
    {
        Debug.Log("El jugador ha caído...");
        // Avisar al Manager
        DueloManager.Instance.CambiarEstado(DueloManager.Instance.GameOverEstado);
    }
}