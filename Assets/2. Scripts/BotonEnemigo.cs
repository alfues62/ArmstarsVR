using UnityEngine;
using UnityEngine.UI; // Necesario para tocar la imagen del botón

public class BotonEnemigo : MonoBehaviour
{
    [Header("1. ¿Quién es este enemigo? (La Ficha)")]
    public DatosEnemigo fichaDeEsteBoton; // Aquí arrastras "El_Bravo.asset"

    [Header("2. ¿Dónde va a aparecer? (El Cuerpo)")]
    public UnidadEnemiga contenedorFisico; // Arrastra aquí al enemigo de la escena (aunque esté oculto)

    [Header("Opcional: Auto-Configurar Imagen")]
    public Image imagenDelBoton; // Arrastra el componente Image de este mismo botón

    void Start()
    {
        // TRUCO PRO: Al iniciar, el botón se pone la foto del enemigo automáticamente
        if (fichaDeEsteBoton != null && imagenDelBoton != null)
        {
            imagenDelBoton.sprite = fichaDeEsteBoton.spriteEnemigo;
        }
    }

    // Esta es la función que conectaremos al click
    public void LucharContraEste()
    {
        if (fichaDeEsteBoton == null)
        {
            Debug.LogError("¡Este botón no tiene ficha de enemigo asignada!");
            return;
        }

        Debug.Log("Has elegido luchar contra: " + fichaDeEsteBoton.nombreEnemigo);

        // 1. Aseguramos que el cuerpo físico sea visible
        contenedorFisico.gameObject.SetActive(true);

        // 2. Llamamos a la función Maestra que creamos antes
        DueloManager.Instance.PrepararYComenzarDuelo(contenedorFisico, fichaDeEsteBoton);

        // 3. Ocultar el menú de selección (mejor usar el Canvas padre)
        // Buscamos el Canvas raíz para apagar todo el menú de golpe
        Canvas canvasRaiz = GetComponentInParent<Canvas>();
        if (canvasRaiz != null) canvasRaiz.gameObject.SetActive(false);
    }
}