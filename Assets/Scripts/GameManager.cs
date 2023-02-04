using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float AverageFitnessLevel = 50f;
    public float MutationIntensity = 20f;
    public float CreatureCap = 20f;
    public bool HighGraphics = true;
    public stateType state = stateType.MENU;
    public float MainVolume = 100f;

    public static GameManager Instance;
    private void Awake()
    {
        //ensures only one instance of gameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
 
    public enum stateType
    {
        MENU,
        PAUSED,
        ACTIVE
    }
}