
using System;
using UnityEngine;

public class PredatorBehaviour : CreatureBehaviour
{
    Vector3 MoveToPerform = Vector3.zero;
    Vector3 checkMove = Vector3.zero;
    public AudioSource DeathNoise;
    public AudioSource KillNoise;
    float startTime = 0.0f;
    float birthTime = 0.0f;

    void Start()
    {
        IsPrey = false;
        birthTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > birthTime + 5* Math.Clamp(speciesStat,5,10))
        {
            DeathNoise.Play();
            this.gameObject.SetActive(false);
        }
        switch (currentState)
        {
            case CreatureSpawner.JSON_Variables.state.IDLE:
                MoveToPerform = idleBehaviour();
                Vector3 direction = checkForCreatures(LayerMask.GetMask("Prey"));
                if (direction != Vector3.zero)
                {
                    chase(direction);
                    this.currentState = CreatureSpawner.JSON_Variables.state.CHASING;
                    startTime = Time.time;
                }
                break;

            case CreatureSpawner.JSON_Variables.state.CHASING:
                checkMove = checkForCreatures(LayerMask.GetMask("Prey"));
                if (checkMove != Vector3.zero)
                {
                    MoveToPerform = chase(checkMove);
                    if (Bite())
                    {
                        KillNoise.Play();
                        currentState = CreatureSpawner.JSON_Variables.state.MULTIPLYING;
                    }
                }
                if (Time.time > startTime + speciesStat)
                {
                    currentState = CreatureSpawner.JSON_Variables.state.IDLE;
                }
                break;

            case CreatureSpawner.JSON_Variables.state.MULTIPLYING:
                checkMove = breedBehaviour();
                if (checkMove != Vector3.zero)
                {
                    MoveToPerform = checkMove;
                }
                else
                {
                    CreateOffspring();
                    currentState = CreatureSpawner.JSON_Variables.state.IDLE;
                }
                break;
            default:
                break;
        }
        TryValidMove(MoveToPerform);
    }

    public Vector3 chase(Vector3 toFace)
    {
        Debug.Log(toFace);
        transform.rotation = Quaternion.LookRotation(new Vector3(toFace.x, 0, toFace.z));
        return (transform.forward * Time.deltaTime * speed * 1.5f);
    }
    public bool Bite()
    {
        for (float angle = -Mathf.PI; angle < Mathf.PI; angle += Convert.ToSingle(step / Math.PI))
        {
            Ray ray = new Ray((transform.position + new Vector3(0, 1, 0)), transform.forward + new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2, LayerMask.GetMask("Prey")))
            {
                hit.collider.gameObject.SetActive(false);
                //play noise
                return (true);
            }
        }
        return (false);
    }
}
