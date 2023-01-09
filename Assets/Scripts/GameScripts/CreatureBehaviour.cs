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
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
        
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,-1,0)*Time.deltaTime*step,Space.Self);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0,1,0)*Time.deltaTime*step,Space.Self);
        }
    }
}
