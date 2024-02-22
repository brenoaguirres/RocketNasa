using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // Variables
    [SerializeField] private float thrustForce = 2.8f;

    public void Thrust(Rigidbody rb) {
        rb.AddForce(Vector3.up * thrustForce, ForceMode.Acceleration);
    }
}
