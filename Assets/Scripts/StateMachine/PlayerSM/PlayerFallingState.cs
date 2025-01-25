using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallingState : PlayerBaseState
{

    private readonly int FallKey = Animator.StringToHash("Fall");
    private const float CrossFadeDuration = 0.1f;
    private Vector3 momentum;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine){}
    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0;
        stateMachine.Animator.CrossFadeInFixedTime(FallKey, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        //apply force
        
        if(stateMachine.CharacterController.isGrounded){
            //Make a function to change between target and free look state
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        
    }

    public override void Exit(){

    }

}
