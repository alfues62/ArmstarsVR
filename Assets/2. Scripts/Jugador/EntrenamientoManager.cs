using UnityEngine;

public class EntrenamientoManager : MonoBehaviour
{
    // Llamar a estas funciones desde botones en la UI de entrenamiento
    public void EntrenarFuerza(int cantidad)
    {
        JugadorStats.Instance.ataqueBase += cantidad;
        Debug.Log($"Ataque aumentado a: {JugadorStats.Instance.ataqueBase}");
    }

    public void EntrenarResistencia(int cantidad)
    {
        JugadorStats.Instance.resistencia += cantidad;
        Debug.Log($"Resistencia aumentada a: {JugadorStats.Instance.resistencia}");
    }
}