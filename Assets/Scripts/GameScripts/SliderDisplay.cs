using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderDisplay : MonoBehaviour
{
    public Slider slider; 
    public TMP_Text value;
    
    void Start()
    {
        changeVals();
    }
    public void changeVals()
    {
        value.text = Convert.ToString(slider.value);
    } 
    
}
