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
    public new string name;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float fitness;
    [SerializeField]
    private float vision = 100;
    [SerializeField]
    private float visionLength;
    [SerializeField]
    private float step;
    public state currentState = state.IDLE;
    void Awake()
    {
        speed = Random.value*10;
        this.gameObject.name = $"{Random.Range(1,1000)}{name}";
    }
    void Update()
    {  
        for (float angle = -Mathf.PI; angle < Mathf.PI; angle+=Mathf.PI/step)
        {
            Debug.DrawRay(transform.position,new Vector3(Mathf.Tan(angle),0,1).normalized*vision,Color.green);
    
        }
    }
    
}
