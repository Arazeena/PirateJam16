using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine){
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime){
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget() {
        if(stateMachine.Targeter.CurrentTarget == null)
            return;
        
        Vector3 dir = stateMachine.Targeter.CurrentTarget.transform.position
                        - stateMachine.transform.position;
        dir.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(dir);
    }
}
