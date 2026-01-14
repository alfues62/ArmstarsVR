using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{

    private Rigidbody[] rigidbodies;
    public Animator animator;
    public Rigidbody headRB;

    public float x_axis;
    public float y_axis;
    public float z_axis;
    public float fuerza;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
        animator.enabled = true;
    }

    void ToggleRagdoll(bool enabled) //True = Ragdoll, False = No Ragdoll
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            animator.enabled = !enabled;
            rigidbody.isKinematic = !enabled;
        }
        headRB.AddForce(x_axis * fuerza, y_axis * fuerza, z_axis * fuerza);

    }

    public void DoRagdoll() {
        ToggleRagdoll(true);
    }

    public void UnDoRagdoll()
    {
        ToggleRagdoll(false);
        animator.Play("idle");
    }

}
