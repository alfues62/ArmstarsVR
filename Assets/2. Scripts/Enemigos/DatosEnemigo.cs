using UnityEngine;

[CreateAssetMenu(fileName = "NuevoEnemigo", menuName = "Duelo/Enemigo")]
public class DatosEnemigo : ScriptableObject
{
    [Header("Identidad")]
    public string nombreEnemigo;
    public Sprite spriteEnemigo;

    [Header("Estadísticas Base")]
    public int vidaMax;
    public int fuerza;
    public int resistencia;
    public int precision;

    [TextArea]
    public string descripcion;
}