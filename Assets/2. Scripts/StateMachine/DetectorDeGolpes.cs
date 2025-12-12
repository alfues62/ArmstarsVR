using UnityEngine;

public class DetectorDeGolpes : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 1. Calcular la velocidad del impacto
        float velocidadDelGolpe = collision.relativeVelocity.magnitude;

        // 2. Obtener el punto de impacto GLOBAL (Coordenada en el mundo)
        Vector3 puntoImpactoGlobal = collision.contacts[0].point;

        // 3. Convertir a LOCAL (Coordenada en el cubo)
        // Esto te dice "en qué parte de la cara del cubo" le diste.
        // Ejem: (0.5, 0.5, 0.5) sería una esquina superior, sin importar dónde esté el cubo en la sala.
        Vector3 puntoImpactoLocal = transform.InverseTransformPoint(puntoImpactoGlobal);

        Debug.Log("Velocidad: " + velocidadDelGolpe);
        Debug.Log("Lugar Local (fijo al cubo): " + puntoImpactoLocal);

        // Lógica de daño
        if (velocidadDelGolpe > 10)
        {
            Debug.Log("¡Golpe crítico! El cubo debería romperse.");
            Destroy(gameObject);
        }
    }
}