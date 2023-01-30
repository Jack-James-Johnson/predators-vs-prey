using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCreatureValues : MonoBehaviour
{
    public TMP_Text UIName;
    public TMP_Text UISpeed;
    public TMP_Text UIVision;
    public TMP_Text UIBreedTime;
    public TMP_Text UIFitness;

    public void ChangeText(GameObject creature)
    {
        var creature_behaviour = creature.GetComponent<CreatureBehaviour>();
        UIName.text = $"{creature.name}";
        UISpeed.text = $"Speed: {creature_behaviour.speed}";
        UIVision.text = $"Vision: {creature_behaviour.vision}";
        UIBreedTime.text = $"Breed Time: {creature_behaviour.breed_time}";
        UIFitness.text = $"Fitness: {new string(creature_behaviour.fitness)}";
    }
    public void HideText()
    {
        UISpeed.text = "";
        UIName.text = "";
        UIVision.text = "";
        UIBreedTime.text = "";
        UIFitness.text = "";
    }
}
