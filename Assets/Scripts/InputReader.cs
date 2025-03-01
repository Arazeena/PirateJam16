using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public event Action DodgeEvent, TargetEvent;
    public Vector2 Movement {get; private set;}

    private Controls controls;
    private void Start(){
            controls = new Controls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
    }

    public void OnDodge(InputAction.CallbackContext context){
        if(!context.performed)
            return;

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context){
        Movement = context.ReadValue<Vector2>();
    }

    public void OnCamera(InputAction.CallbackContext context){
        //Implemented with CineMachine
    }

    private void OnDestroy(){
        controls.Player.Disable();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed)
            return;
        
        TargetEvent?.Invoke();
    }
}
