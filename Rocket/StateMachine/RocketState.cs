using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketState : MonoBehaviour
{
    protected RocketController rocketController;
    protected RocketStateMachine rocketStateMachine;

    public RocketState(RocketController rocketController, RocketStateMachine rocketStateMachine) {
        this.rocketController = rocketController;
        this.rocketStateMachine = rocketStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void SoundTriggerEvent(RocketController.SoundTriggerType triggerType) { }
}
