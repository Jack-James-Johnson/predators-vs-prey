using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatureBehaviour : MonoBehaviour
{
    public float vision;
    public float speed;
    public float breed_time;
    public float speciesStat;
    public char[] fitness;
    public bool IsPrey = true;
    public CreatureSpawner.JSON_Variables.state currentState = CreatureSpawner.JSON_Variables.state.IDLE;
    protected float step = 0.1f;
    float initTime = 0;
    public float idleRandom = 1;
    private GameManager gameManager;
    private GameObject originalPrefab;
    public GameObject spawner;
    void Awake()
    {
        // Give each creature access to the game manager variables
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Generate the stats of each creature independently
        GenerateStats();

        if (IsPrey)
        {
            originalPrefab = GameObject.Find("Prey");
        }
        else
        {
            originalPrefab = GameObject.Find("Predator");
        }
    }
    public void TryValidMove(Vector3 move)
    {
        Vector3 targetPos = transform.position + move;
        if ((targetPos.x > 50 || targetPos.x < -50) || targetPos.z > 50 || targetPos.z < -50)
        {
            transform.Rotate(0, 180, 0);
            transform.position += transform.forward;
        }
        else
        {
            transform.position += move;
        }
    }
    protected Vector3 checkForCreatures(LayerMask mask)
    {
        float mostFavourableTarget = 0;
        Vector3 currentDirection = Vector3.zero;
        for (float angle = -Mathf.PI; angle < Mathf.PI; angle += Convert.ToSingle(step / Math.PI))
        {
            Ray ray = new Ray((transform.position + new Vector3(0, 1, 0)), transform.forward + new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, vision * 5, mask))
            {
                CreatureBehaviour creatureHit = hit.collider.gameObject.GetComponent<CreatureBehaviour>();
                float favourScore = Convert.ToInt32(new string(creatureHit.fitness), 2) / hit.distance;
                if (mostFavourableTarget < favourScore)
                {
                    mostFavourableTarget = favourScore;
                    currentDirection = ray.direction;
                }
            }
        }
        return (currentDirection);
    }
    protected Vector3 idleBehaviour()
    {
        if (initTime == 0)
        {
            initTime = Time.time;
        }
        else if (Time.time > initTime + speciesStat)
        {
            idleRandom = UnityEngine.Random.Range(-10, 10);
            initTime = 0;
        }
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime * idleRandom);
        return (transform.forward * Time.deltaTime * speed);
    }
    public Vector3 breedBehaviour()
    {
        if (initTime == 0)
        {
            initTime = Time.time;
        }
        else if (Time.time > initTime + 16 - breed_time)
        {
            initTime = 0;
            return (Vector3.zero);
        }
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime * 5);
        return (transform.forward * Time.deltaTime);
    }
    public void GenerateStats(string PFitness = "0000000000000000")
    {
        // Generate the stats of each creature by utilising an 2 byte binary string
        this.fitness = PFitness.ToCharArray();

        // Iterate through the until enough values are changed according to the AverageFitness level
        if (PFitness == "0000000000000000")
        {

            for (int _ = 0; _ < Mathf.RoundToInt(Mathf.Clamp(gameManager.AverageFitnessLevel / 10, 3, 11)); _++)
            {
                // Choose random index of fitness string
                int index = Mathf.RoundToInt(UnityEngine.Random.Range(1, 17));
                ;
                // If string is not already one, change to one
                if (index - 1 != '1')
                {
                    fitness[index - 1] = '1';
                }
                // else, decrement to account for the same index
                else
                {
                    _--;
                }

            }
        }
        // Sets the variables according to the bits on the fitness value 
        this.speed = Mathf.Clamp(Convert.ToInt32($"{fitness[0]}{fitness[1]}{fitness[2]}{fitness[3]}", 2), 1, 16);
        this.vision = Mathf.Clamp(Convert.ToInt32($"{fitness[4]}{fitness[5]}{fitness[6]}{fitness[7]}", 2), 1, 16);
        this.breed_time = Mathf.Clamp(Convert.ToInt32($"{fitness[8]}{fitness[9]}{fitness[10]}{fitness[11]}", 2), 1, 16);
        this.speciesStat = Mathf.Clamp(Convert.ToInt32($"{fitness[12]}{fitness[13]}{fitness[14]}{fitness[15]}", 2), 1, 16);

        // Creates custom colour based on the fitness value
        Color CustomColour = new Color(speed, vision, breed_time);

        // Change the colour of the creature according to it's fitness value
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.SetColor("_Color", CustomColour);
        }
    }
    public void CreateOffspring()
    {
        GameObject creatureObj = Instantiate(originalPrefab, transform);
        creatureObj.SetActive(true);
        creatureObj.transform.SetParent(spawner.transform);
        CreatureBehaviour creature = creatureObj.GetComponent<CreatureBehaviour>();
        char[] newFitness = creature.fitness;
        for (int i = 0; i < 16; i++)
        {
            if (UnityEngine.Random.Range(1, 100) < gameManager.MutationIntensity)
            {
                newFitness[i] = (newFitness[i] == '1' ? '0' : '1');
            }
        }
        creature.GenerateStats(new string(newFitness));
        creatureObj.name = $"{this.name} jr.";
        //set the first child (high graphic model) active is highGraphics == true
        creatureObj.transform.GetChild(0).gameObject.SetActive(gameManager.HighGraphics);
        // set the other child (low graphic model) active if highGraphics == false 
        creatureObj.transform.GetChild(1).gameObject.SetActive(!gameManager.HighGraphics);
        creature.currentState = CreatureSpawner.JSON_Variables.state.IDLE;
    }
}
