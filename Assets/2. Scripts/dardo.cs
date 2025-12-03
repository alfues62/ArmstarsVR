using UnityEngine;
using System.Collections;

public class dardo : MonoBehaviour
{
    public Rigidbody dartRigidBody;

    public bool inCollider = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!inCollider)
        {
            dartRigidBody.isKinematic = true;
            inCollider = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        inCollider=false;
    }
}
