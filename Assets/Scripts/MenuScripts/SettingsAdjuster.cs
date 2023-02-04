using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAdjuster : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }
     public void OnMainChange(float main)
    {
        gameManager.MainVolume=main;
    }
    public void OnMusicChange(float music)
    {
        gameManager.GetComponent<AudioSource>().volume=music;
    }
     public void OnFitnessChange(float fitness)
    {
        gameManager.AverageFitnessLevel = fitness;
    }
    public void OnMutationChange(float mutation)
    {
        gameManager.MutationIntensity = mutation;
    }
    public void OnCreatureCap(float creatureCap)
    {
        gameManager.CreatureCap = creatureCap;
    }
    public void ToggleGraphics(bool graphics)
    {
        gameManager.HighGraphics = graphics;
    }

}
