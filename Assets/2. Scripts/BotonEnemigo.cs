using UnityEngine;
using UnityEngine.UI;

public class BotonEnemigo : MonoBehaviour
{
    [Header("Ficha de este enemigo")]
    public DatosEnemigo fichaDeEsteBoton;

    [Header("Referencias")]
    public Activador scriptActivador; // Arrastra aquí el objeto que tiene el script Activador
    public Image imagenDelBoton;

    void Start()
    {
        if (fichaDeEsteBoton != null && imagenDelBoton != null)
        {
            imagenDelBoton.sprite = fichaDeEsteBoton.spriteEnemigo;
        }
    }

    public void LucharContraEste()
    {

        Debug.Log("He clickado");
        if (fichaDeEsteBoton == null || scriptActivador == null)
        {
            Debug.LogError("Faltan datos o referencia al Activador en el botón.");
            return;
        }

        // 1. Simulamos la tecla 'K' enviando los datos al activador
        scriptActivador.CargarEnemigoDesdeUI(fichaDeEsteBoton);

        // 2. Cerramos el menú para que el jugador pueda pulsar 'L'
        Canvas canvasRaiz = GetComponentInParent<Canvas>();
        if (canvasRaiz != null) canvasRaiz.gameObject.SetActive(false);
    }
}