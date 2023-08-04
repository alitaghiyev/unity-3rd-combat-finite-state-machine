using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUi : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private Camera myCam;
    [SerializeField] private GameObject gameOverPanel;
    private void Start()
    {
        myCam = Camera.main;
    }
    public void SetHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
        if(currentHealth <=0){
           gameOverPanel.SetActive(true);
        }
    }
}
