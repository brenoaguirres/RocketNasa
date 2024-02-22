using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketThrustingState : RocketState
{
    public RocketThrustingState(RocketController rocketController, RocketStateMachine rocketStateMachine) : base(rocketController, rocketStateMachine)
    {
    }
}
