using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LOCOMOTIONBLENDHASH = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossfadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;


    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LOCOMOTIONBLENDHASH, CrossfadeDuration);
    }


    public override void UpdateState(float deltaTime)
    {
        if (!IsInChaseRange())//eger player uzaksa idle state gecilecek
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttacingState(stateMachine));
            return;
        }
        MoveToPlayer(deltaTime);//aski halde player takip edilecek
        FaceToPlayer();
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void ExitState()
    {//chasing state den cikarken resetleme
       if (stateMachine.Agent.isOnNavMesh)
        {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
        }

    }

    private void MoveToPlayer(float deltaTime)
    {//navmesh yardimi ile playere dogru ilerleme
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    private bool IsInAttackRange()
    {
        if(stateMachine.Player.IsDead) return false;
        float playerDistanceSQR = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSQR <= stateMachine.PlayerChasingRange * stateMachine.AttackingRange;
    }
}
