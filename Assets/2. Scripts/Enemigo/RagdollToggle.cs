using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{

    private Rigidbody[] rigidbodies;
    public Animator animator;

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

    }

    public void DoRagdoll() {
        ToggleRagdoll(true);
    }

    public void UnDoRagdoll()
    {
        ToggleRagdoll(false);
        animator.Play("idle");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            UnDoRagdoll();
        }
    }

}
