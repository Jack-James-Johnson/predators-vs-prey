using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject creaturePrefab;
    private GameManager gameManager;
    public GameObject Stage;
    private float MaxHeight;
    private float MaxWidth;
    public void Awake()
    {
        // Obtain max stage height and width
        MaxHeight = Stage.transform.localScale.x/2;
        MaxWidth = Stage.transform.localScale.z/2;

        //find instanced Game Manager Object
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        changeModels(gameManager.HighGraphics);
        InitSummon();
        creaturePrefab.SetActive(false);
    }

    private void InitSummon()
    {
        for (int _ = 0; _ < gameManager.CreatureCap; _++)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-MaxHeight, MaxHeight), 1, Random.Range(-MaxWidth, MaxWidth));
            var creature = Instantiate(creaturePrefab, SpawnPosition, Quaternion.identity, this.transform);
        }
    }
    private void changeModels(bool highGraphics)
    {
        creaturePrefab.transform.GetChild(0).gameObject.SetActive(highGraphics);
        creaturePrefab.transform.GetChild(1).gameObject.SetActive(!highGraphics);
    } 
}
