using UnityEngine;
using System.Collections;

public class CuerdaMaquinaGym : MonoBehaviour
{

    public GameObject pivotPoint;
    public GameObject anchorMachine;
    public GameObject restartPosition;

    public Vector3 positionOriginal;
    public Vector3 scaleOriginal;

    public bool isGrabbed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionOriginal = restartPosition.transform.localPosition;
        scaleOriginal = pivotPoint.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed){
            UpdateRotation();
            UpdateScale();
            //vamos a hacer que el restartPosition siga a la mano para poder calcular facilmente el angulo
            restartPosition.transform.position = pivotPoint.transform.position;
        }
    }
    
    private void UpdateRotation()
    {
        pivotPoint.transform.LookAt(anchorMachine.transform);

    }
    
    private void UpdateScale()
    {
        float distancia = Vector3.Distance(restartPosition.transform.localPosition, anchorMachine.transform.localPosition);

        pivotPoint.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * distancia * (15f/0.3f));
        Debug.Log(distancia);
    }
    
    public void RestartPosScaleOfRope()
    {
        transform.localPosition = positionOriginal;
        transform.localRotation = Quaternion.identity;
        pivotPoint.transform.localScale = scaleOriginal;
        UpdateRotation();
    }

    public void setIsGrabbed(bool grabbed){
        isGrabbed = grabbed;
        if(!grabbed){
            RestartPosScaleOfRope();
        }
    }
}
