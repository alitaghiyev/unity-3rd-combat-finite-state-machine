using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State {
    
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime) {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime) {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Facetarget() {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }
        Vector3 lookPosition = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPosition.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }


    // protected void ReturntLocomotion(){
    //     if(stateMachine.Targeter.CurrentTarget != null){
    //         stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    //     }
    //     else{
    //         stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    //     }
    // }

}
