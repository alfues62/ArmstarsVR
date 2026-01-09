using UnityEngine;

public class DetectorDeGolpes : MonoBehaviour
{
    // Variables para guardar la "foto" del primer golpe
    private bool golpeRegistrado = false;
    private float velocidadGuardada;
    private Vector3 puntoLocalGuardado;

    // 1. ESTA ES LA FUNCIÓN QUE DETECTA EL GOLPE (Automática)
    void OnCollisionEnter(Collision collision)
    {
        // Si ya tenemos un golpe guardado en este turno, ignoramos los siguientes rebotes
        if (golpeRegistrado) return;

        // --- CAPTURA DE DATOS ---
        velocidadGuardada = collision.relativeVelocity.magnitude;

        Vector3 puntoImpactoGlobal = collision.contacts[0].point;
        puntoLocalGuardado = transform.InverseTransformPoint(puntoImpactoGlobal);
        Debug.Log($"[DetectorDeGolpes] Golpe detectado con velocidad {velocidadGuardada} en punto local {puntoLocalGuardado}");
        // --- BLOQUEO ---
        // Cerramos la puerta para que no entren más datos hasta que reiniciemos
        golpeRegistrado = true;
    }

    // 2. FUNCIÓN PARA QUE EL MANAGER PIDA LOS DATOS
    // Devuelve true si hay un golpe guardado, y saca los datos por las variables 'out'
    public bool IntentarObtenerGolpe(out float velocidad, out Vector3 puntoLocal)
    {
        if (golpeRegistrado)
        {
            velocidad = velocidadGuardada;
            puntoLocal = puntoLocalGuardado;
            return true; // "¡Tengo datos!"
        }
        else
        {
            velocidad = 0f;
            puntoLocal = Vector3.zero;
            return false; // "Aún no me han pegado"
        }
    }

    // 3. FUNCIÓN PARA REINICIAR EN EL SIGUIENTE TURNO
    // Llamar a esto desde 'EstadoTurnoJugador.Entrar()'
    public void PrepararNuevoTurno()
    {
        golpeRegistrado = false;
        velocidadGuardada = 0f;
        puntoLocalGuardado = Vector3.zero;
    }
}