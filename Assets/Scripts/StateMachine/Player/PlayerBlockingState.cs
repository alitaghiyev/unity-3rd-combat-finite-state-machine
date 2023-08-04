using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{

    private readonly int BlockHash = Animator.StringToHash("Block");
    private const float CrossfadeDuration = 0.1f;


    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        stateMachine.health.SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossfadeDuration);
    }

    public override void UpdateState(float deltaTime)
    {
        Move(deltaTime);
        if (!stateMachine.InputReader.IsBlocking)
        {//
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

    }
    public override void ExitState()
    {
        stateMachine.health.SetInvulnerable(false);
    }
}
