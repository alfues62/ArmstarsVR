using UnityEngine;
using UnityEngine.VFX;

public class ExplosionSalivaVFX : MonoBehaviour
{
    public VisualEffect ExplosionSaliva;

    // Esta es la función que debe llamar tu otro script
    public void EjecutarExplosionSaliva(Vector3 puntoImpacto)
    {
        // 1. Movemos el objeto del VFX al lugar exacto del golpe (la mejilla) 
        ExplosionSaliva.transform.position = puntoImpacto;

        // 2. Reiniciamos el sistema por si acaso quedó algo pendiente
        ExplosionSaliva.Reinit();

        // 3. Enviamos el evento de Play. 
        // Como en el VFX Graph usamos un "Single Burst", solo soltará una ráfaga y parará.
        ExplosionSaliva.SendEvent("OnPlay");
    }
}
