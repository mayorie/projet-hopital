using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
