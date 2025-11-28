using UnityEngine;

public class DetectorArmwrestlingCompleto : MonoBehaviour
{
    [Header("Referencias")]
    public Transform mandoDerecha; // Arrastra tu Controller aquí

    [Header("Lecturas en Tiempo Real (Solo lectura)")]
    public float gradosCupping;   // Eje X: Flexión de muñeca
    public float gradosRising;    // Eje Y: Nudillos arriba
    public float gradosPronacion; // Eje Z: Giro de muñeca

    [Header("Configuración de Umbrales")]
    public float umbralCupping = 20f;
    public float umbralRising = 15f;  // El Rising suele requerir menos ángulo
    public float umbralPronacion = 25f;

    [Header("Controles")]
    public KeyCode teclaCalibrar = KeyCode.Space;

    // Variables internas para matemáticas de rotación
    private Quaternion rotacionInicial;
    private bool estaCalibrado = false;

    // Estados para evitar spam en consola
    private string ultimoEstado = "";

    void Start()
    {
        if (mandoDerecha != null) Calibrar();
    }

    void Update()
    {
        if (mandoDerecha == null) return;

        // Calibrar manual (clave para empezar el round)
        if (Input.GetKeyDown(teclaCalibrar))
        {
            Calibrar();
        }

        if (estaCalibrado)
        {
            CalcularMovimientos();
        }
    }

    public void Calibrar()
    {
        // Guardamos la rotación exacta del momento "Ready... Go!"
        rotacionInicial = mandoDerecha.localRotation;
        estaCalibrado = true;
        Debug.Log("<color=green>CALIBRADO: Posición Neutra Establecida.</color>");
    }

    void CalcularMovimientos()
    {
        // 1. OBTENER LA DIFERENCIA
        // Comparamos la rotación actual con la inicial
        Quaternion diferencia = Quaternion.Inverse(rotacionInicial) * mandoDerecha.localRotation;
        Vector3 angulos = diferencia.eulerAngles;

        // 2. NORMALIZAR (Convertir 360 a 0, etc.)
        // Asignamos a las variables públicas para verlas en el Inspector
        gradosCupping = NormalizarAngulo(angulos.x);
        gradosRising = NormalizarAngulo(angulos.y); // Eje Y es el Rising
        gradosPronacion = NormalizarAngulo(angulos.z);

        // 3. DETECTAR Y MOSTRAR ESTADOS
        // Creamos un string para ver qué está haciendo el jugador
        string estadoActual = "";

        // --- DETECCIÓN DE RISING (Eje Y) ---
        // Nota: Dependiendo del mando (Quest 2 vs 3 vs Index), 
        // el Rising puede ser Y positivo o Y negativo.
        // Prueba: Haz Rising. Si el valor 'gradosRising' es positivo, usa >. Si es negativo, usa <.
        if (gradosRising > umbralRising)
        {
            estadoActual += "[RISING ACTIVO] ";
        }
        else if (gradosRising < -umbralRising)
        {
            estadoActual += "[SINKING (Bajando)] "; // Lo contrario al Rising (Ulnar Deviation)
        }

        // --- DETECCIÓN DE CUPPING (Eje X) ---
        // Nota: Igual que antes, verifica el signo.
        if (gradosCupping < -umbralCupping)
        {
            estadoActual += "[CUPPING] ";
        }

        // --- DETECCIÓN DE PRONACIÓN (Eje Z) ---
        if (gradosPronacion > umbralPronacion) estadoActual += "[PRONACIÓN] ";
        else if (gradosPronacion < -umbralPronacion) estadoActual += "[SUPINACIÓN] ";

        // LOGGING INTELIGENTE (Solo si hay cambios y hay algo activo)
        if (estadoActual != "" && estadoActual != ultimoEstado)
        {
            Debug.Log($"<color=yellow>MOVIMIENTO: {estadoActual}</color>");
            ultimoEstado = estadoActual;
        }
        else if (estadoActual == "" && ultimoEstado != "Neutro")
        {
            Debug.Log("Neutro");
            ultimoEstado = "Neutro";
        }
    }

    float NormalizarAngulo(float angulo)
    {
        if (angulo > 180) return angulo - 360;
        return angulo;
    }
}