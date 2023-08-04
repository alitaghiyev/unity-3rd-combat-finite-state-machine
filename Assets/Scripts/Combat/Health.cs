using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


[System.Serializable]
public class GameEvent<T0,T1> : UnityEvent<T0,T1>{} //generic event class


public class Health : MonoBehaviour
{
    public event Action TakeDamageHandler;
    
    public event Action OnDieHandler;

    public GameEvent<float, float> HealthEvent;

    [SerializeField] private int maxHealth=100;

     private bool hasShield;

     public bool IsDead =>health ==0;//health degeri 0 ise true dönür
    private int health;

    private void Start() {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable){
        this.hasShield = isInvulnerable;
    }
    public void CalculateDamage(int damage){
        if(health ==0) return;
        if(hasShield) return;
        health = Mathf.Max(health-damage,0);
        TakeDamageHandler?.Invoke();
        if(health==0){
            OnDieHandler?.Invoke();
        }
        HealthEvent?.Invoke(health,maxHealth);
        print(health);
        //yuakrıda aşağıdakı kodun tek satırlık hali gösterilmiştir bu durumda ilk if kontrolunde <=0 yerine ==0 kontrol edile bilir
        // health -= damage;
        // if(health <0){
        //     health =0;
        // }
    }
}
