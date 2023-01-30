using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatureBehaviour : MonoBehaviour
{
    public enum state
    {
        IDLE,
        CHASING,
        RUNNING,
        MULTIPLYING
    }
    public float vision;
    public float speed;
    public float breed_time;
    public char[] fitness;
    private float step = 0.1f;
    private GameManager gameManager;
    public state currentState = state.IDLE;
    public string uniqueName;
    public Vector3 pos;
    void Awake()
    {
        // Give each creature access to the game manager variables
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Generate the stats of each creature independently
        GenerateStats();
    }
    void Update()
    {

        for (float angle = -Mathf.PI; angle < Mathf.PI; angle += Convert.ToSingle(step/Math.PI))
        {
            Debug.DrawRay(transform.position, new Vector3(Mathf.Tan(angle), 0, 1).normalized * vision, Color.green);
        }
    }
    public void GenerateStats()
    {
        // Generate the stats of each creature by utilising an 2 byte binary string
        this.fitness = "0000000000000000".ToCharArray();

        // Iterate through the until enough values are changed according to the AverageFitness level
        for (int _ = 0; _ < Mathf.RoundToInt(Mathf.Clamp(gameManager.AverageFitnessLevel / 10, 3, 11)); _++)
        {
            // Choose random index of fitness string
            int index = Mathf.RoundToInt(UnityEngine.Random.Range(1, 17));
;            
            // If string is not already one, change to one
            if (index-1 != '1')
            {
                fitness[index-1] = '1';
            }
            // else, decrement to account for the same index
            else
            {
                _--;
            }

        }
        // Sets the variables according to the bits on the fitness value 
        this.speed = Convert.ToInt32($"{fitness[0]}{fitness[1]}{fitness[2]}{fitness[3]}", 2);
        this.vision = Convert.ToInt32($"{fitness[4]}{fitness[5]}{fitness[6]}{fitness[7]}",2);
        this.breed_time = Convert.ToInt32($"{fitness[8]}{fitness[9]}{fitness[10]}{fitness[11]}",2);
        this.vision = Convert.ToInt32($"{fitness[12]}{fitness[13]}{fitness[14]}{fitness[15]}",2);
        
        // Creates custom colour based on the fitness value
        Color CustomColour = new Color(speed,vision,breed_time);

        // Change the colour of the creature according to it's fitness value
        foreach(Transform child in transform)
        {
            child.GetComponent<Renderer>().material.SetColor("_Color",CustomColour);
        }
    }
}
