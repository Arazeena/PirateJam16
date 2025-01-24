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
        //Move to movement state?
        Vector2 moveDir = stateMachine.InputReader.Movement;
        Vector3 movement = new Vector3(stateMachine.InputReader.Movement.x, 0, stateMachine.InputReader.Movement.y).normalized;
        stateMachine.CharacterController.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);
        if(stateMachine.InputReader.Movement == Vector2.zero) return;
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);

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
