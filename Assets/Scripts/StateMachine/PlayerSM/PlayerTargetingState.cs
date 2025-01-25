using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetBlendTree = Animator.StringToHash("TargetBlendTree");
    private readonly int CombatForwardSpeedKey = Animator.StringToHash("CombatForwardSpeed");
    private readonly int CombatStrafeSpeedKey = Animator.StringToHash("CombatStrafeSpeed");
    
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

        FaceTarget();
        Vector3 movement = CalculateMovement();
        UpdateAnimator(deltaTime);
        Move(movement * stateMachine.CombatMovementSpeed, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= Cancel;
    }

    private void Cancel(){
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement(){
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.Movement.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.Movement.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime){
        Vector2 movement = stateMachine.InputReader.Movement;
        if(movement.x != 0)
            movement.x = movement.x > 0f ? 1f : -1f;
        if(movement.y != 0)
            movement.y = movement.y > 0f ? 1f : -1f;

        stateMachine.Animator.SetFloat(CombatStrafeSpeedKey, movement.x, 0.1f, deltaTime);
        stateMachine.Animator.SetFloat(CombatForwardSpeedKey, movement.y, 0.1f, deltaTime);
    }

}
