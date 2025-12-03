using UnityEngine;
using System.Collections;

public class dardo : MonoBehaviour
{
    public Rigidbody dartRigidBody;
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
        dartRigidBody.isKinematic = true;
    }
}
