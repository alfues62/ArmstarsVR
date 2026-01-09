using UnityEngine;

public class DetectorDeGolpes : MonoBehaviour
{
    // El "candado" que asegura un solo golpe por turno
    public bool golpeRegistrado = false;

    private float velocidadGuardada;
    private Vector3 puntoLocalGuardado;

    void OnCollisionEnter(Collision collision)
    {
        // 1. Si el candado está cerrado, ignoramos rebotes
        if (golpeRegistrado) return;

        // 2. Capturamos los datos del impacto
        velocidadGuardada = collision.relativeVelocity.magnitude;
        Vector3 puntoImpactoGlobal = collision.contacts[0].point;

        // Convertimos a local por si queremos detectar puntos débiles relativos al enemigo
        puntoLocalGuardado = transform.InverseTransformPoint(puntoImpactoGlobal);

        Debug.Log($"[Detector] ¡IMPACTO! Vel: {velocidadGuardada:F1}");

        // 3. CERRAMOS EL CANDADO (Vital para evitar doble daño)
        golpeRegistrado = true;
    }

    // El EstadoTurnoJugador llamará a esto
    public bool IntentarObtenerGolpe(out float velocidad, out Vector3 puntoLocal)
    {
        if (golpeRegistrado)
        {
            velocidad = velocidadGuardada;
            puntoLocal = puntoLocalGuardado;
            return true;
        }
        else
        {
            velocidad = 0f;
            puntoLocal = Vector3.zero;
            return false;
        }
    }

    // Se llama al iniciar el turno del jugador para permitir un nuevo golpe
    public void PrepararNuevoTurno()
    {
        golpeRegistrado = false;
    }
}