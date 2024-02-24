using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketController : MonoBehaviour
{
    #region Constants

    private const float minHeightAirborne = 1f;
    private const float parachuteSpawnHeight = 20f;
    private const float groundCheckDistance = 0.1f;

    #endregion

    #region Variables

    [SerializeField] private bool enginesActivated = false;
    private bool hasFuel = true;
    private bool isAirborne = false;
    [SerializeField] private float maxHeightReached = 0f;
    private bool canSpawnCapsule = true;
    [SerializeField] private float parachuteDrag = 5f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded = true;

    #endregion
 
    #region Cached

    private RocketMovement rocketMovement;
    private RocketFuel rocketFuel;
    private RocketRotation rocketRotation;
    private Rigidbody rb;
    private Parachute parachute;
    private RocketVFXHandler vFXHandler;
    private RocketAudioController audioController;
    
    #endregion

    void Awake()
    {
        rocketFuel = GetComponent<RocketFuel>();
        rocketMovement = GetComponent<RocketMovement>();
        rocketRotation = GetComponent<RocketRotation>();
        rb = GetComponent<Rigidbody>();
        parachute = GetComponent<Parachute>();
        vFXHandler = GetComponent<RocketVFXHandler>();
        audioController = GetComponent<RocketAudioController>();
    }
    
    void FixedUpdate()
    {
        IsGrounded();
        SaveMaxHeight();
        MoveRocket();
        //DelayCharge();
        RotateRocket();
    }


    #region Methods

    public void ActivateEngine() {
        enginesActivated = true;
    }

    public void MoveRocket() {
        if (hasFuel && enginesActivated) {
            hasFuel = rocketFuel.SpendFuel();
            rocketMovement.Thrust(rb);
            CallVFX();
            CallSFX();
        }
        else if (!hasFuel && !enginesActivated && transform.position.y <= 
                    parachuteSpawnHeight && canSpawnCapsule) {
            SpawnParachute();
        }
        
        if (!hasFuel && enginesActivated) {
            DeactivateEngine();
        }
    }

    public void RotateRocket() {
        if (!isGrounded && canSpawnCapsule)
            rocketRotation.RotateTowards(rb);
        
        if (!canSpawnCapsule) {
            rocketRotation.ParachuteRotation(rb);
        }
    }

    public void DelayCharge() {
        if (transform.rotation.eulerAngles.x >= 340f && transform.rotation.eulerAngles.x >= 345f && !isGrounded) {
            rocketFuel.Refuel();
        }

        if (hasFuel && !enginesActivated && !isGrounded)
            CallVFX(1, true);
        else
            CallVFX(1, false);

        //hasFuel = rocketFuel.SpendFuel();
    }

    public void DeactivateEngine() {
        hasFuel = false;
        enginesActivated = false;
        CallVFX(0, false);
        CallSFX(0, false, false);
    }

    public void SaveMaxHeight() {
        if (transform.position.y > minHeightAirborne)
            isAirborne = true;
        else
            isAirborne = false;

        if (isAirborne && rb.velocity.y > 0)
            maxHeightReached = transform.position.y;
    }

    public bool IsGrounded() {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, groundCheckDistance, groundMask)) {
            isGrounded = true;
            return true;
        }
        else {
            isGrounded = false;
            return false;
        }
    }

    public void SpawnParachute() {
        canSpawnCapsule = false;
        rb.drag = parachuteDrag;
        CallSFX(1, true, false);
        parachute.Spawn();
    }

    public void CallVFX() {
        // Starting Effects
        if (isGrounded) {
            vFXHandler.ToggleVFX(0, true);
            vFXHandler.ToggleVFX(1, true);
            vFXHandler.ToggleVFX(2, true);
        }
        else {
            vFXHandler.ToggleVFX(1, false);
            vFXHandler.ToggleVFX(2, false);
        }
    }

    public void CallVFX(int index, bool toggle) {
        vFXHandler.ToggleVFX(index, toggle);
    }

    public void CallSFX() {
        audioController.PlaySFX(0, true);
    }

    public void CallSFX(int index, bool toggle, bool loop) {
        audioController.ToggleSFX(index, loop, toggle);
    }

    #endregion
}
