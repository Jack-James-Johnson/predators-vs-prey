using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCreatureValues : MonoBehaviour
{
    public TMP_Text UIName;
    public TMP_Text UISpeed;
    public TMP_Text UIVision;
    public TMP_Text UIBreedSpeed;
    public TMP_Text UISpecies;

    public void ChangeText(GameObject creature)
    {
        var creature_behaviour = creature.GetComponent<CreatureBehaviour>();
        UIName.text = $"{creature.name}";
        UISpeed.text = $"Speed: {creature_behaviour.speed}";
        UIVision.text = $"Vision: {creature_behaviour.vision}";
        UIBreedSpeed.text = $"Breed Speed: {creature_behaviour.breed_time}";
        UISpecies.text = creature_behaviour.IsPrey ? "Prey" : "Predator";
    }
    public void HideText()
    {
        UISpeed.text = "";
        UIName.text = "";
        UIVision.text = "";
        UIBreedSpeed.text = "";
        UISpecies.text = "";
    }
}
