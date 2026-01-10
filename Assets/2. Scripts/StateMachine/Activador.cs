using UnityEngine;

public class Activador : MonoBehaviour
{
    [Header("Configuración de Prueba")]
    public UnidadEnemiga cuerpoEnEscena;
    public EnemyBeaten scriptBeaten;
    public DatosEnemigo fichaDeDatos;

    private bool enemigoCargado = false;

    void Update()
    {
        // TECLA K: Carga visual
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (cuerpoEnEscena != null && fichaDeDatos != null)
            {
                cuerpoEnEscena.gameObject.SetActive(true);
                scriptBeaten.UnBeatEnemy();
                cuerpoEnEscena.ConfigurarEnemigo(fichaDeDatos);
                enemigoCargado = true;
                Debug.Log("[Activador] Enemigo cargado. Pulsa 'L' para iniciar combate.");
            }
        }

        // TECLA L: Inicio Lógico
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (enemigoCargado && DueloManager.Instance != null)
            {
                DueloManager.Instance.PrepararYComenzarDuelo(cuerpoEnEscena, fichaDeDatos);
                enemigoCargado = false; // Bloquear reinicio accidental
            }
            else
            {
                Debug.LogWarning("[Activador] Pulsa 'K' primero para cargar al enemigo.");
            }
        }
    }
}