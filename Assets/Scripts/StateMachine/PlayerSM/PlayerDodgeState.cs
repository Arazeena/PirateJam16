using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    float dodgeTime = 1f;
    public PlayerDodgeState(PlayerStateMachine stateMachine) : base(stateMachine){    
    }

    public override void Enter()
    {
        Debug.Log("Dodging");
    }
    public override void Tick(float deltaTime)
    {
        if(dodgeTime <= 0){
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            return;
        }

        dodgeTime -= deltaTime;
    }
    public override void Exit()
    {
        Debug.Log("Dodge done");
        
    }
}
