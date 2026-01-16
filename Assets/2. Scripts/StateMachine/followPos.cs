using UnityEngine;

public class followPos : MonoBehaviour
{

    private Transform target;
    private Rigidbody rb;

    private void Start()
    {
        target = transform.parent;
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = (target.position - this.transform.position) / Time.fixedDeltaTime;
        Quaternion quaternionDifference = target.rotation * Quaternion.Inverse(this.transform.rotation);
        quaternionDifference.ToAngleAxis(out float angleDegrees, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleDegrees * rotationAxis;
        rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
    
}