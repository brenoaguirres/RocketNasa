using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotation : MonoBehaviour
{
    // Variables
    public float rotationSpeed = 30f;

    public void RotateTowards(Rigidbody rb) {
        if (rb.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
