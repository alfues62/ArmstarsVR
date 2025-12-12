using UnityEngine;

public abstract class EstadoDuelo : MonoBehaviour
{
    protected DueloManager manager;
    public virtual void Inicializar(DueloManager _manager)
    {
        manager = _manager;
    }

    // Se ejecuta una vez cuando el estado empieza
    public virtual void Entrar() { }

    // Se ejecuta en cada frame (loop del juego)
    public virtual void Actualizar() { }

    // Se ejecuta una vez cuando el estado termina
    public virtual void Salir() { }
}