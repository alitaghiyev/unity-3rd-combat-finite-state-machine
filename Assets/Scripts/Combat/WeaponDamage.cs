using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
   [SerializeField] private Collider myCollider;

    private List<Collider> alraedyCollideWith = new();
    private int damage;
    private float knockback;

    private void OnEnable() {
        alraedyCollideWith.Clear();
    }
   private void OnTriggerEnter(Collider other) {
    if(other == myCollider || alraedyCollideWith.Contains(other)) return;
    alraedyCollideWith.Add(other);
    if(other.TryGetComponent(out Health health)){
           health.CalculateDamage(damage);
    }
    if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver)){
        forceReceiver.AddForce((other.transform.position - myCollider.transform.position).normalized *  knockback);
    }
   }
    public void SetAttack(int damage, float knockback){
        this.damage = damage;
        this.knockback = knockback;
    }


}
