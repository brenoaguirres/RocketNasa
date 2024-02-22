using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketIdleState : RocketState
{
    #region Variables

    [SerializeField] private bool enginesActivated = false;
    private bool hasFuel = true;

    #endregion

    
    public RocketIdleState(RocketController rocketController, RocketStateMachine rocketStateMachine) : base(rocketController, rocketStateMachine)
    {
    }

    public override void SoundTriggerEvent(RocketController.SoundTriggerType triggerType) { 
        base.SoundTriggerEvent(triggerType);
    }
    public override void EnterState() {
        base.EnterState();
    }
    public override void ExitState() { 
        base.ExitState();
    }
    public override void FrameUpdate() { 
        base.FrameUpdate();

        if (hasFuel && enginesActivated) {
            rocketController.StateMachine.ChangeState(rocketController.ThrustingState);
        }
    }
    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();
    }
}
