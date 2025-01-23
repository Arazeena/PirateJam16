using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine){    
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }
    public override void Tick(float deltaTime)
    {
        Debug.Log("Tick");
    }
    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
