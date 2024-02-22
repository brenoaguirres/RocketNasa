using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCoastingFlightState : RocketState
{
    public RocketCoastingFlightState(RocketController rocketController, RocketStateMachine rocketStateMachine) : base(rocketController, rocketStateMachine)
    {
    }
}
