using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }

    [field: SerializeField] public float RotationDampValue { get; private set; }
     public Transform MainCamTransform { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health health { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }


    private void Start()
    {
        MainCamTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    
    private void OnEnable() {
        health.TakeDamageHandler+= HandletakeDamage;
        health.OnDieHandler += HandleDie;
    }
    private void OnDisable() {
        health.TakeDamageHandler-= HandletakeDamage;
        health.OnDieHandler -= HandleDie;
    }

    private void HandletakeDamage(){
        SwitchState(new PlayerImpactState(this));
    }
       private void HandleDie(){
        SwitchState(new PlayerDeadState(this));
    }
}
