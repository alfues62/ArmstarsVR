using UnityEngine;

public class DetectorArmwrestlingVR : MonoBehaviour
{
    [Header("Referencias")]
    public Transform camaraCabeza;
    public Transform manoDerecha;

    [Header("Sensibilidad - Cupping (Flexión)")]
    [Tooltip("Grados necesarios para considerar que hay Cupping.")]
    public float umbralCupping = 25.0f; // 25 grados de flexión

    [Header("Sensibilidad - Otros")]
    public float umbralBrazoExtendido = 0.25f;

    // Estados para evitar spam en consola
    private string ultimoEstadoRotacion = "";
    private string ultimoEstadoCupping = "";

    void Update()
    {
        if (manoDerecha != null)
        {
            DetectarPronacionSupinacion();
            DetectarCupping();
        }
    }

    void DetectarCupping()
    {
        // Obtenemos el ángulo X (Flexión/Extensión)
        float anguloX = NormalizarAngulo(manoDerecha.localEulerAngles.x);

        string estadoActual = "Muñeca: Recta";

        // LÓGICA DE CUPPING (AJUSTAR SIGNO SEGÚN TU MANDO)
        // En la mayoría de mandos (Oculus/Meta):
        // Rotar la muñeca hacia adentro (Cupping) suele generar ángulos negativos o positivos
        // dependiendo de cómo Unity interprete el eje local del mando.

        // Asumiremos aquí que:
        // > X positivo = Extension (Kickback / Knuckles Up)
        // < X negativo = Flexion (Cupping / Hook in)
        // IMPORTANTE: Si te sale al revés, cambia el '<' por '>' abajo.

        if (anguloX < -umbralCupping)
        {
            estadoActual = "🔥 CUPPING DETECTADO (Hook)";
        }
        else if (anguloX > umbralCupping)
        {
            estadoActual = "Muñeca: Kickback (Extensión)";
        }

        // Debug solo si cambia el estado
        if (estadoActual != ultimoEstadoCupping)
        {
            Debug.Log(estadoActual);
            ultimoEstadoCupping = estadoActual;
        }
    }

    void DetectarPronacionSupinacion()
    {
        // Usamos el ángulo Z normalizado (-180 a 180)
        float anguloZ = NormalizarAngulo(manoDerecha.localEulerAngles.z);
        string estadoActual = "Rotación: NEUTRO";

        // Rango de tolerancia de +/- 25 grados para neutro
        if (anguloZ > 25)
        {
            // Hacia un lado es Pronación
            // (En Unity Z positivo suele ser hacia la izquierda/adentro para mano derecha)
            estadoActual = "Rotación: PRONACIÓN (Toproll/Press)";
        }
        else if (anguloZ < -25)
        {
            estadoActual = "Rotación: SUPINACIÓN (Defensa/Bicep)";
        }

        if (estadoActual != ultimoEstadoRotacion)
        {
            Debug.Log(estadoActual);
            ultimoEstadoRotacion = estadoActual;
        }
    }

    // --- HELPER FUNCTION ---
    // Convierte los grados de 0-360 a -180 a 180.
    // Esto hace mucho más fácil saber si vas a la "izquierda" o "derecha" del centro.
    float NormalizarAngulo(float angulo)
    {
        if (angulo > 180)
            return angulo - 360;
        return angulo;
    }
}