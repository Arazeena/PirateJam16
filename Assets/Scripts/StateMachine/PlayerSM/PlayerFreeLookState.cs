using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookSpeedKey = Animator.StringToHash("FreeLookMovementSpeed");
    private readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");
    private float animDamping = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){    
    }

    public override void Enter()
    {
        stateMachine.Animator.Play(FreeLookBlendTree);
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.TargetEvent += OnTarget;
        Debug.Log("Free Roam");
    }
    public override void Tick(float deltaTime)
    {
        if(!stateMachine.CharacterController.isGrounded && stateMachine.CharacterController.velocity.y < 0){
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        Vector3 movement = CalculateLookDirection();
        stateMachine.CharacterController.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);
        if(stateMachine.InputReader.Movement == Vector2.zero) {
            stateMachine.Animator.SetFloat(FreeLookSpeedKey, 0, animDamping, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedKey, 1, animDamping, deltaTime);
        FaceMovementDirection(movement, deltaTime);

    }
    public override void Exit()
    {
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnDodge(){
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine));
    }

    private void OnTarget(){
        if(!stateMachine.Targeter.SelectTarget())
            return;
        
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateLookDirection(){
        Vector3 camForward = stateMachine.CamPos.forward;
        Vector3 camRight = stateMachine.CamPos.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * stateMachine.InputReader.Movement.y +
        camRight * stateMachine.InputReader.Movement.x;
    } 

    private void FaceMovementDirection(Vector3 movement, float deltaTime){
        stateMachine.transform.rotation = Quaternion.Lerp
            (stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            stateMachine.RotationSmooth * deltaTime);
    }
}
