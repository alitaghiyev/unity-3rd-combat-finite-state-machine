using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ForceReceiver : MonoBehaviour {
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = .3f;
    private Vector3 impact;//saldırı yonunde itme kuvveti

    private float verticalVelocity;

    private Vector3 dampingVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    private void Update() {

        VerticalVelocity();
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
        if(agent !=null){
            if(impact.sqrMagnitude < .2f*.2f){//.2
            impact = Vector3.zero;
            agent.enabled = true;
        }
        // if(impact == Vector3.zero){
        //     agent.enabled = true;
        // }
        }
    }

    private void VerticalVelocity() {
        if (verticalVelocity < 0f && controller.isGrounded) {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
    public void AddForce(Vector3 force) {
        impact += force;
        if(agent != null){
            agent.enabled = false;
        }
    }
}
