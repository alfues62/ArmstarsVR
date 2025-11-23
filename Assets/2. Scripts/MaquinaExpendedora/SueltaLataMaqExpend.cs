using UnityEngine;

public class SueltaLataMaqExpend : MonoBehaviour
{

    public GameObject cukeCanPrefab;
    public Transform canSpawnPoint;

    public int canLimit = 20;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnPoint.childCount >= canLimit)
        {
            Destroy(canSpawnPoint.GetChild(0).gameObject);
        }
    }

    private float randomBetweenLimits(float minNum, float range){
        // equacion: Random * RangoDelRandom + ValorMin
        // ej: queremos entre 70 y 75 -> Random.value*5 + 70
        return ((Random.value * range) + minNum);
    }

    public void SpawnCukeCan(){
        float rotX = randomBetweenLimits(-40f, 80f);
        float rotY = randomBetweenLimits(-40f, 80f);
        float rotZ = randomBetweenLimits(50f, 80f);

        //(prefab, position, rotation, parent)
        Instantiate(cukeCanPrefab, canSpawnPoint.position, Quaternion.Euler(rotX, rotY, rotZ), canSpawnPoint);
    }
}
