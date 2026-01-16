using UnityEngine;

public class DetectorArmwrestlingPro : MonoBehaviour
{
    [Header("Referencias")]
    public Transform mandoDerecha;

    [Header("Estado en Tiempo Real")]
    public float gradosCupping;   // Flexión de muñeca
    public float gradosPronacion; // Rotación de muñeca

    [Header("Configuración")]
    public float umbralCupping = 20f;
    public KeyCode teclaCalibrar = KeyCode.Space; // Para probar en el editor

    // Guardamos la rotación exacta de cuando empieza el combate
    private Quaternion rotacionInicial;
    private bool estaCalibrado = false;

    void Start()
    {
        // Si no hay calibración manual, calibrar al inicio
        if (mandoDerecha != null) Calibrar();
    }

    void Update()
    {
        if (mandoDerecha == null) return;

        // Opción para recalibrar cuando quieras (ej. al reiniciar el round)
        if (Input.GetKeyDown(teclaCalibrar))
        {
            Calibrar();
        }

        if (estaCalibrado)
        {
            CalcularMovimientosRelativos();
        }
    }

    public void Calibrar()
    {
        // Guardamos la rotación local actual como el "Cero"
        rotacionInicial = mandoDerecha.localRotation;
        estaCalibrado = true;
        Debug.Log("CALIBRADO: Posición Neutra establecida.");
    }

    void CalcularMovimientosRelativos()
    {
        // MATEMÁTICA DE QUATERNIONES:
        // Calculamos la diferencia entre la rotación actual y la inicial.
        // "Quaternion.Inverse" es como restar rotaciones.
        Quaternion diferencia = Quaternion.Inverse(rotacionInicial) * mandoDerecha.localRotation;

        // Convertimos esa diferencia a ángulos comprensibles
        Vector3 angulosDiferencia = diferencia.eulerAngles;

        // Normalizamos los ángulos (de 0..360 a -180..180) para leerlos fácil
        gradosCupping = NormalizarAngulo(angulosDiferencia.x);
        gradosPronacion = NormalizarAngulo(angulosDiferencia.z);

        // --- DETECCIÓN ---

        // Detección de Cupping (Ahora es mucho más estable)
        // Nota: El signo puede variar según el mando, prueba si es > o <
        if (gradosCupping < -umbralCupping)
        {
            Debug.Log($"CUPPING ACTIVO ({gradosCupping:F1}°)");
        }

        // Detección de Pronación
        if (gradosPronacion > 25) Debug.Log("PRONACIÓN");
        if (gradosPronacion < -25) Debug.Log("SUPINACIÓN");
    }

    float NormalizarAngulo(float angulo)
    {
        if (angulo > 180) return angulo - 360;
        return angulo;
    }
}