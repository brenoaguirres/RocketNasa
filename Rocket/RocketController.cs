using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    #region Constants

    private const float minHeightAirborne = 1f;
    private const float parachuteSpawnHeight = 20f;
    private const float rotationMinHeight = 1.5f;

    #endregion

    #region Variables

    [SerializeField] private bool enginesActivated = false;
    private bool hasFuel = true;
    private bool isAirborne = false;
    [SerializeField] private float maxHeightReached = 0f;
    private bool canSpawnCapsule = true;
    [SerializeField] private float parachuteDrag = 5f;

    #endregion
 
    #region Cached

    private RocketMovement rocketMovement;
    private RocketFuel rocketFuel;
    private RocketRotation rocketRotation;
    private Rigidbody rb;
    private Parachute parachute;
    
    #endregion

    void Awake()
    {
        rocketFuel = GetComponent<RocketFuel>();
        rocketMovement = GetComponent<RocketMovement>();
        rocketRotation = GetComponent<RocketRotation>();
        rb = GetComponent<Rigidbody>();
        parachute = GetComponent<Parachute>();
    }
    
    void FixedUpdate()
    {
        VerifyMaxHeight();

        if (hasFuel && enginesActivated) {
            rocketFuel.SpendFuel();
            rocketMovement.Thrust(rb);
        }
        else if (!hasFuel && !enginesActivated && transform.position.y <= 
                    parachuteSpawnHeight && canSpawnCapsule) {
            SpawnParachute();
        }

        if (transform.position.y >= rotationMinHeight)
            rocketRotation.RotateTowards(rb);
    }

    public void FuelDepleted() {
        hasFuel = false;
        enginesActivated = false;
    }

    public void VerifyMaxHeight() {
        if (transform.position.y > minHeightAirborne)
            isAirborne = true;
        else
            isAirborne = false;

        if (isAirborne && rb.velocity.y > 0)
            maxHeightReached = transform.position.y;
    }

    public void SpawnParachute() {
        canSpawnCapsule = false;
        rb.drag = parachuteDrag;
        parachute.Spawn();
    }
}
