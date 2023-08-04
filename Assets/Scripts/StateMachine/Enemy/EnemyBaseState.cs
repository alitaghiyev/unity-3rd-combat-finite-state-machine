using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected bool IsInChaseRange()//playere kadar olan uzaklığı hesaplama
    {
         if(stateMachine.Player.IsDead) return false;
        float playerDistanceSQR = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSQR <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;//optimize 
        // Vector3 toPlayer = stateMachine.Player.transform.position- stateMachine.transform.position;
        // return toPlayer.magnitude <=stateMachine.PlayerChasingRange;
    }

    protected void FaceToPlayer()//playere doğru dönme
    {
        if (stateMachine.Player == null) { return; }
        Vector3 looPosition = stateMachine.Player.transform.position - stateMachine.transform.position;
        looPosition.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(looPosition);
    }
}
