using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject creaturePrefab;
    private GameManager gameManager;
    public GameObject Stage;
    public GameObject spawner;
    private float MaxHeight;
    private float MaxWidth;
    static string m_path;
    static string adj_path;
    static string noun_path;
    
    static string[] Adjectives;
    static string[] Nouns;
    
    public void Awake()
    {
        // Obtain max stage height and width
        MaxHeight = Stage.transform.localScale.x/2;
        MaxWidth = Stage.transform.localScale.z/2;

        //find instanced Game Manager Object
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_path = Application.dataPath;
        adj_path = $"{m_path}/Scripts/scriptResources/Wordlist-Adjectives-Common-Audited-Len-3-6.txt";
        noun_path = $"{m_path}/Scripts/scriptResources/Wordlist-Nouns-Common-Audited-Len-3-6.txt";

        Adjectives = System.IO.File.ReadAllLines(adj_path);
        Nouns = System.IO.File.ReadAllLines(noun_path);
        
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
            creature.name = generateString();

        }
    }
    private void changeModels(bool highGraphics)
    {
        creaturePrefab.transform.GetChild(0).gameObject.SetActive(highGraphics);
        creaturePrefab.transform.GetChild(1).gameObject.SetActive(!highGraphics);
    }
    private string generateString()
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        string adjective =textInfo.ToTitleCase(Adjectives[UnityEngine.Random.Range(0,Adjectives.Length)]);
        string noun = textInfo.ToTitleCase(Nouns[UnityEngine.Random.Range(0,Nouns.Length)]);
        return($"{adjective} {noun}");
    } 
     private void SaveGame()
     {
     }
}
