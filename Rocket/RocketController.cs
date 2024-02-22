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

    #region State Machine Variables

    public RocketStateMachine StateMachine {get; set;}
    public RocketIdleState IdleState {get; set;}
    public RocketThrustingState ThrustingState {get; set;}
    public RocketCoastingFlightState CoastingFlightState {get; set;}
    public RocketDelayChargeState DelayChargeState {get; set;}
    public RocketParachuteState ParachuteState {get; set;}

    #endregion

    void Awake()
    {
        rocketFuel = GetComponent<RocketFuel>();
        rocketMovement = GetComponent<RocketMovement>();
        rocketRotation = GetComponent<RocketRotation>();
        rb = GetComponent<Rigidbody>();
        parachute = GetComponent<Parachute>();

        // Set-up state machine
        StateMachine = new RocketStateMachine();
        IdleState = new RocketIdleState(this, StateMachine);
        ThrustingState = new RocketThrustingState(this, StateMachine);
        CoastingFlightState = new RocketCoastingFlightState(this, StateMachine);
        DelayChargeState = new RocketDelayChargeState(this, StateMachine);
        ParachuteState = new RocketParachuteState(this, StateMachine);

    }

    void Update() {
        StateMachine.CurrentRocketState.FrameUpdate();
    }
    
    void FixedUpdate()
    {
        StateMachine.CurrentRocketState.PhysicsUpdate();

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

    #region Particles

    // boilerplate

    #endregion

    #region Sounds

    public void SoundTriggerEvent(SoundTriggerType triggerType) {
        StateMachine.CurrentRocketState.SoundTriggerEvent(triggerType);
    }

    public enum SoundTriggerType
    {
        RocketThrust,
        ParachuteOpening
    }

    #endregion
}
