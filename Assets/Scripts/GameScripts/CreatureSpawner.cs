using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject creaturePrefab;
    private GameManager gameManager;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InitSummon();
    }

    private void InitSummon()
    {
        for (int _ = 0;_ <  gameManager.CreatureCap; _++)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-24,24),1,Random.Range(-24,24));
            Instantiate(creaturePrefab,SpawnPosition,Quaternion.identity);
            
        }
    }
}
