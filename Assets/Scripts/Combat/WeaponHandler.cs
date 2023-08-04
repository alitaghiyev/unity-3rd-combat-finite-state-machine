using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//burada aniamtion eventler yardımı ile saldiri zamani  silahın colliderinin acilib kapanma islemi yapliyor.
public class WeaponHandler : MonoBehaviour
{
   [SerializeField] private GameObject weaponHitbox;
    
    public void EnableWeaponHitbox(){
        weaponHitbox.SetActive(true);
    }
     public void DisableWeaponHitbox(){
        weaponHitbox.SetActive(false);
    }
}
