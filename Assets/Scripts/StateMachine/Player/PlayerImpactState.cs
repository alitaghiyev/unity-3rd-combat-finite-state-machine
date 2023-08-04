using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{

    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossfadeDuration = 0.1f;
    private float duration = 1f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossfadeDuration);
    }
    public override void UpdateState(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <= 0) {ReturnMovement();}
    }
    public override void ExitState() { }
       private void ReturnMovement(){ 
        if(stateMachine.Targeter.CurrentTarget != null){
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else{
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
