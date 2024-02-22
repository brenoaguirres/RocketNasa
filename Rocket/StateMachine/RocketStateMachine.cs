using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStateMachine : MonoBehaviour
{
    public RocketState CurrentRocketState { get; set; }
    public void Initialize(RocketState startingState) {
        CurrentRocketState = startingState;
        CurrentRocketState.EnterState();
    }

    public void ChangeState(RocketState newState) {
        CurrentRocketState.ExitState();
        CurrentRocketState = newState;
        CurrentRocketState.EnterState();
    }
}
