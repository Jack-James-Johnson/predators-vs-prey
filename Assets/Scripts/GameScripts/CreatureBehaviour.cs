using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatureBehaviour : MonoBehaviour
{
    private enum state
    {
        IDLE,
        CHASING,
        RUNNING,
        MULTIPLYING
    }
    [SerializeField]
    private float speed;
    [SerializeField]
    private float fitness;
    [SerializeField]
    private float vision;
    [SerializeField]
    private float visionLength;
    [SerializeField]
    private float step;
    private state currentState = state.IDLE;
    void Awake()
    {
        speed = Random.value*10;
    }
    void Update()
    {  
        for (float angle = -Mathf.PI; angle < Mathf.PI; angle+=Mathf.PI/step)
        {
            Debug.DrawRay(transform.position,new Vector3(Mathf.Tan(angle),0,1).normalized*vision,Color.green);
        }
    }
}
