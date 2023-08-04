using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState {

    private float previousFrameTime;

    private bool alreadyAppliedForce;
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void EnterState() {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,attack.TransitionDuration);
    }
    public override void UpdateState(float deltaTime) {

        Move(deltaTime);
        Facetarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f) {

            if(normalizedTime >= attack.ForceTime){
                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking) {
                TryComboAttack(normalizedTime);
            }
        }
        else {
            if(stateMachine.Targeter.CurrentTarget != null){
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else{
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }
    public override void ExitState() {}
    private void TryComboAttack(float normalizedTime) {
        if(attack.ComboStateIndex == -1) { return; }

        if(normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(
            new PlayerAttackingState(stateMachine, attack.ComboStateIndex)
            );
    }

    private void TryApplyForce(){
        if(alreadyAppliedForce)return;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward* attack.Force);
        alreadyAppliedForce = true;
    }

}
