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
        // if refresh rate of monitor is 0 (disabled by OS)
        if(refreshRate==0)
        {
            frameRateSlider.transform.Translate(Screen.width,0,0,Space.World);
        }
        //otherwise the maximum framerate is monitor's refresh rate times 2 
        frameRateSlider.maxValue = refreshRate*2;
    }
    public void ChangeFrameRate(float targetFrameRate)
    {
        // if the current value of the slider is the minimum value of the slider
        if(targetFrameRate ==frameRateSlider.minValue)
        {
            // set target frame rate to -1 (enables V-SYNC within unity) 
            Application.targetFrameRate = -1;
            // enable V-Sync in unity settings
            QualitySettings.vSyncCount = 1;
            //change the slider text from a number to V-Sync 
            FrameRate.text = "V-Sync";
        }
        else
        {
            //set the target frame rate to the number of slider
            Application.targetFrameRate = Convert.ToInt32(targetFrameRate);
            //disable v-sync
            QualitySettings.vSyncCount = 0;
            //slider text is the value of the slider
            FrameRate.text = $"{targetFrameRate}"; 
        }
    }
}
