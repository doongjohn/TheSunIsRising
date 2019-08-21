using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ASlider : AUI
{
    [SerializeField] string OnSliderChange;
    Slider slider;

    public float Value { get => slider.value; set => slider.value = value; }
    public float Min { get => slider.minValue; set => slider.minValue = value; }
    public float Max { get => slider.maxValue; set => slider.maxValue = value; }


    protected override void Awake() 
    {
        EventPath = OnSliderChange;
        base.Awake();
        slider = GetComponent<Slider>();
    }
    
    public void ThisOnSliderChange()
    {
        EvAct();
    }


}