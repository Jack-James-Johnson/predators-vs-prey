using System;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehaviour : CreatureBehaviour
{
    Vector3 MoveToPerform = Vector3.zero;
    Vector3 DirectionToFlee = Vector3.zero;
    float SinceLastBreed = 0.0f;

    float startTime;
    void Start()
    {
        IsPrey = true;
        SinceLastBreed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > SinceLastBreed + 5 * Math.Clamp(speciesStat, 5, 10))
        {
            CreateOffspring();
            SinceLastBreed = Time.time;
        }
        switch (currentState)
        {
            case CreatureSpawner.JSON_Variables.state.IDLE:
                MoveToPerform = idleBehaviour();
                Vector3 check = checkForCreatures(LayerMask.GetMask("Predator"));
                if (check != Vector3.zero)
                {
                    transform.Rotate(0, 180 / Mathf.PI * Mathf.Tan(check.z / check.x), 0);
                    currentState = CreatureSpawner.JSON_Variables.state.RUNNING;
                    startTime = Time.time;
                }
                break;
            case CreatureSpawner.JSON_Variables.state.RUNNING:
                MoveToPerform = transform.forward * Time.deltaTime * speed * 1.5f;
                // if creature has been running for speciesStat seconds
                if (Time.time > startTime + speciesStat)
                {
                    currentState = CreatureSpawner.JSON_Variables.state.IDLE;
                }
                break;
            case CreatureSpawner.JSON_Variables.state.MULTIPLYING:
                Vector3 move = breedBehaviour();
                if (move != Vector3.zero)
                {
                    MoveToPerform = move;
                }
                else
                {
                    currentState = CreatureSpawner.JSON_Variables.state.IDLE;
                }
                break;
            default:
                break;
        }
        TryValidMove(MoveToPerform);
    }
}