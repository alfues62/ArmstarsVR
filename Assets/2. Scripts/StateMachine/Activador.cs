using UnityEngine;

public class Activador : MonoBehaviour
{
    [Header("Configuración de Prueba")]
    public UnidadEnemiga cuerpoEnEscena;
    public EnemyBeaten scriptBeaten;
    public DatosEnemigo fichaDeDatos;

    private bool enemigoCargado = false;

    // --- NUEVA FUNCIÓN PARA LA UI ---
    public void CargarEnemigoDesdeUI(DatosEnemigo nuevaFicha)
    {
        fichaDeDatos = nuevaFicha; // Guardamos la ficha que viene del botón
        
        if (cuerpoEnEscena != null && fichaDeDatos != null)
        {
            cuerpoEnEscena.gameObject.SetActive(true);
            scriptBeaten.UnBeatEnemy();
            cuerpoEnEscena.ConfigurarEnemigo(fichaDeDatos);
            enemigoCargado = true;
            Debug.Log($"[Activador] {fichaDeDatos.nombreEnemigo} cargado desde UI. Pulsa 'L' para luchar.");
        }
    }

    void Update()
    {
        // TECLA K: Sigue funcionando por si quieres probar manual
        if (Input.GetKeyDown(KeyCode.K))
        {
            CargarEnemigoDesdeUI(fichaDeDatos);
        }

        // TECLA L: Inicio Lógico (Sigue igual que antes)
        if (Input.GetKeyDown(KeyCode.L))
        {
            iniciarDueloActivador();
        }
    }

    public void iniciarDueloActivador()
    {
        if (enemigoCargado && DueloManager.Instance != null)
        {
            DueloManager.Instance.PrepararYComenzarDuelo(cuerpoEnEscena, fichaDeDatos);
            enemigoCargado = false;
            Debug.Log("[Activador] ¡Duelo Iniciado!");
        }
        else
        {
            Debug.LogWarning("[Activador] Primero elige un enemigo en la UI.");
        }
    }
}