using UnityEngine;

[CreateAssetMenu(fileName = "NuevoEnemigo", menuName = "Duelo/Enemigo")]
public class DatosEnemigo : ScriptableObject
{
    [Header("Identidad")]
    public string nombreEnemigo;
    public Sprite spriteEnemigo;

    [Header("Estadísticas Base")]
    public int vidaMax;
    public int ataque; // Daño base antes de la precisión

    [Header("Precisión y Variabilidad")]
    [Range(0f, 1f)] 
    public float precisionBase = 0.7f; // Ejemplo: 0.7 es el 70% (El centro de la campana)
    
    [Range(0f, 0.5f)]
    public float variabilidad = 0.1f; // Ejemplo: 0.1 significa que puede variar un 10% arriba o abajo

    [Header("Descripción")]
    [TextArea]
    public string descripcion;
}