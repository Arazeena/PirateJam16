using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public event Action DodgeEvent;

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
        Debug.Log("Dodge");
    }

    private void OnDestroy(){
        controls.Player.Disable();
    }

}
