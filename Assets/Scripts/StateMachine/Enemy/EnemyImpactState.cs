using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossfadeDuration= 0.1f;

    private  float duration= 1f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossfadeDuration);
    }

    public override void UpdateState(float deltaTime) {
        Move(deltaTime);
        duration -= deltaTime;
        if(duration <=0){
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
     }

    public override void ExitState() { }
}
