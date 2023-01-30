using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float AverageFitnessLevel;
    public float MutationIntensity;
    public int CreatureCap;
    public bool HighGraphics;
    public stateType state = stateType.MENU;

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
    public void changeVals()
    {
        this.AverageFitnessLevel =GameObject.Find("Fitness").GetComponent<Slider>().value;
        this.CreatureCap = Convert.ToInt32(GameObject.Find("Cap").GetComponent<Slider>().value);
        this.MutationIntensity =GameObject.Find("Mutation").GetComponent<Slider>().value;
        
    }
    public void toggleGraphics(bool graphics)
    {
        this.HighGraphics = graphics;
    }
    public enum stateType
    {
        MENU,
        PAUSED,
        ACTIVE
    }
}