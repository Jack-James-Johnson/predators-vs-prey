using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAdjuster : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        //Gets current instance of game manager
        gameManager = GameManager.Instance;
    }
    //separate functions for every slider 
     public void OnMainChange(float main)
    {//called when main volume slider is changed, changing the main volume attribute in game manager
        gameManager.MainVolume=main;
    }
    public void OnMusicChange(float music)
    {//called when music volume slider is changed, changing the music volume attribute in game manager
        gameManager.GetComponent<AudioSource>().volume=music;
    }
     public void OnFitnessChange(float fitness)
    {//called when the fitness slider is changed, changing the fitness  attribute in game manager 
        gameManager.AverageFitnessLevel = fitness;
    }
    public void OnMutationChange(float mutation)
    {//called when the mutation slider is changed, changing the mutation  attribute in game manager
        gameManager.MutationIntensity = mutation; 
    }
    public void OnCreatureCap(float creatureCap)
    {//called when the creature cap slider is changed, changing the creature cap attribute in game manager
        gameManager.CreatureCap = creatureCap;
    }
    public void ToggleGraphics(bool graphics)
    {//called when the toggle graphics button is toggled, changing the graphics setting in the game manager
        gameManager.HighGraphics = graphics;
    }
}