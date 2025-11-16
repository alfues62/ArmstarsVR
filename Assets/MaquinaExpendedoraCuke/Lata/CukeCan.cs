using UnityEngine;

public class CukeCan : MonoBehaviour
{
    public bool isEmpty = false;
    public ParticleSystem dropletsParticules;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectedTest(){
        Debug.Log("Seleccionado");
    }

    public void activatedTest(){
        Debug.Log("Activado");
    }

    public void spilLiquid()
    {
        if (!isEmpty)
        {
            dropletsParticules.Play();
            isEmpty = true;
        }
    }
}
