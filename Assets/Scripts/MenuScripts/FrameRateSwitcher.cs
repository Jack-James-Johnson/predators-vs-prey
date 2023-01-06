using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FrameRateSwitcher : MonoBehaviour
{
    public TMP_Text FrameRate;
    int refreshRate;
    public Slider frameRateSlider;
    private void Awake()
    {
        refreshRate = Screen.currentResolution.refreshRate;
        if(refreshRate==0)
        {
            frameRateSlider.transform.Translate(Screen.width,0,0,Space.World);
        }
        frameRateSlider.maxValue = refreshRate*2;
    }
    public void ChangeFrameRate(float targetFrameRate)
    {
        if(targetFrameRate == 0)
        {
            Application.targetFrameRate = Convert.ToInt32(-1);
            QualitySettings.vSyncCount = 1;
            FrameRate.text = "V-Sync";
        }
        else
        {
            Application.targetFrameRate = Convert.ToInt32(targetFrameRate);
            QualitySettings.vSyncCount = 0;
            FrameRate.text = $"{targetFrameRate}"; 
        }
    }
}
