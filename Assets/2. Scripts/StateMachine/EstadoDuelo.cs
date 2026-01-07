using UnityEngine;

// Es vital que sea 'public' y 'abstract'
public abstract class EstadoDuelo : MonoBehaviour
{
    protected DueloManager manager;

    public virtual void Inicializar(DueloManager _manager)
    {
        manager = _manager;
    }

    public virtual void Entrar() { }

    public virtual void Actualizar() { }

    public virtual void Salir() { }
}