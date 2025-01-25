using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; } 
    public Transform CamPos {get; private set;}

    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float CombatMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSmooth {get; private set; }
    

    private void Start(){
        SwitchState(new PlayerFreeLookState(this));
        CamPos = Camera.main.transform;
    }
}
