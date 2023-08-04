using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float transitionDuration = .1f;
    public EnemyAttacingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, transitionDuration);
    }


    public override void UpdateState(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            FaceToPlayer();

        }
    }
    public override void ExitState()
    {
    }

}
