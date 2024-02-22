using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    // Constants
    private string rocketTag = "Rocket";
    // Variables
    [SerializeField] float windForce = 0f;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag(rocketTag)) {
            var hitObj = other.gameObject;
            if(hitObj != null) {
                var rb = hitObj.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.forward * windForce, ForceMode.Force);
            }
        }
    }
}
