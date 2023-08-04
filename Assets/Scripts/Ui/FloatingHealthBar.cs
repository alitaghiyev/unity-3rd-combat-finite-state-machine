using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Camera myCam;

    private void Start()
    {
        myCam = Camera.main;
    }
 
    public void SetHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
        if(currentHealth <=0){
            healthSlider.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
      healthSlider.transform.LookAt(myCam.transform);
    }
}
