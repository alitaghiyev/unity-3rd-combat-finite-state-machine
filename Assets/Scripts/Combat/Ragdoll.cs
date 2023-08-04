using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//burada 
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] colliders;
    private Rigidbody[] rigidbodies;
    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        rigidbodies = GetComponentsInChildren<Rigidbody>(true);
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
         foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }
        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}
