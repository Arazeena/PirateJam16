using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine){    
    }

    public override void Enter()
    {
        stateMachine.InputReader.DodgeEvent += OnDodge;
        Debug.Log("Enter");
    }
    public override void Tick(float deltaTime)
    {
        Debug.Log("Tick");
    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        Debug.Log("Exit");
    }

    private void OnDodge(){
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine));
    }
}
