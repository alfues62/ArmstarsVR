using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{

    private Rigidbody[] rigidbodies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
    }

    void ToggleRagdoll(bool enabled) //True = Ragdoll, False = No Ragdoll
    {
        bool isKinematic = !enabled;
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = isKinematic;
        }

    }

    public void DoRagdoll() {
        ToggleRagdoll(true);
    }

}
