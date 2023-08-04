using UnityEngine;

public class PlayerTargetingState : PlayerBaseState {
    private readonly int TARGETINGBLENDTREE = Animator.StringToHash("TargetingBlendTree");


    private readonly int TARGETINGFORWARD = Animator.StringToHash("TargetingForward");
    private readonly int TARGETINGRIGHT = Animator.StringToHash("TargetingRight");
    private const float CrossfadeDuration= 0.1f;


    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState() {
        stateMachine.Animator.CrossFadeInFixedTime(TARGETINGBLENDTREE, CrossfadeDuration);
        stateMachine.InputReader.CancelEvent += OnCancel;
    }
    
    public override void UpdateState(float deltaTime) {

        if(stateMachine.InputReader.IsAttacking) {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        if(stateMachine.InputReader.IsBlocking){
             stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null) {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        Facetarget();
        TargetingAnimation(deltaTime);
    }
    public override void ExitState() {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }
    private void OnCancel() {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement() {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        return movement;
    }
    private void TargetingAnimation(float deltaTime) {
        if (stateMachine.InputReader.MovementValue.y == 0) {
            stateMachine.Animator.SetFloat(TARGETINGFORWARD, 0, 0.1f, deltaTime);
        }
        else {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TARGETINGFORWARD, value, 0.1f, deltaTime);
        }
        if (stateMachine.InputReader.MovementValue.x == 0) {
            stateMachine.Animator.SetFloat(TARGETINGRIGHT, 0, 0.1f, deltaTime);
        }
        else {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TARGETINGRIGHT, value, 0.1f, deltaTime);
        }
    }
}
