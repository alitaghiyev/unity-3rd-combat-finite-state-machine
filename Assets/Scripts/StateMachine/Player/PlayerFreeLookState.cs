using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FREELOOKBLENDTREE= Animator.StringToHash("FreeLookBlendTree");
    private readonly int FREELOOKSPEED = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossfadeDuration= 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.CrossFadeInFixedTime(FREELOOKBLENDTREE, CrossfadeDuration);

    }
        public override void ExitState()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }
    public override void UpdateState(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking) {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FREELOOKSPEED, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FREELOOKSPEED, 1, AnimatorDampTime, deltaTime);

        FaceMoveDirection(movement, deltaTime);
    }



    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        return forward * stateMachine.InputReader.MovementValue.y +
        right * stateMachine.InputReader.MovementValue.x;
    }
    private void FaceMoveDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
         Quaternion.LookRotation(movement),
         deltaTime * stateMachine.RotationDampValue);
    }
}
