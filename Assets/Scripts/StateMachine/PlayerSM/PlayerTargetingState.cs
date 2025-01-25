using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetBlendTree = Animator.StringToHash("TargetBlendTree");
    
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.Play(TargetBlendTree);
        stateMachine.InputReader.TargetEvent += Cancel;
        Debug.Log("Targetting");
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.Targeter.CurrentTarget == null){
            Cancel();
            return;
        }
        
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= Cancel;
    }

    private void Cancel(){
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
