using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System;

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
    public const string fileName = "creature";
    public bool NewGame = true;
    public void Start()
    {
        // Obtain max stage height and width
        MaxHeight = Stage.transform.localScale.x/2;
        MaxWidth = Stage.transform.localScale.z/2;

        //find instanced Game Manager Object, to allowing the script to utilise variables 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_path = Application.dataPath;
        adj_path = $"{m_path}/Scripts/scriptResources/Wordlist-Adjectives-Common-Audited-Len-3-6.txt";
        noun_path = $"{m_path}/Scripts/scriptResources/Wordlist-Nouns-Common-Audited-Len-3-6.txt";

        Adjectives = System.IO.File.ReadAllLines(adj_path);
        Nouns = System.IO.File.ReadAllLines(noun_path);
        
        changeModels(gameManager.HighGraphics);
        if(NewGame)
        {
            InitSummon();
            NewGame =false;
        }
        creaturePrefab.SetActive(false);
    }

    private void InitSummon()
    {
        for (int _ = 0; _ < gameManager.CreatureCap; _++)
        {
            Vector3 SpawnPosition = new Vector3(UnityEngine.Random.Range(-MaxHeight, MaxHeight), 1, UnityEngine.Random.Range(-MaxWidth, MaxWidth));
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
    public void SaveGame()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int secondsSinceEpoch = (int)t.TotalSeconds;
        var SaveDirectoryBundle = Application.persistentDataPath + $"/SaveData/{secondsSinceEpoch}/";
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            var creature_behaviour = child.GetComponent<CreatureBehaviour>();
            JSON_Variables jSON_Variables = new JSON_Variables();
            jSON_Variables.Fitness = creature_behaviour.fitness;
            jSON_Variables.Name = creature_behaviour.name;
            jSON_Variables.State = creature_behaviour.currentState;
            jSON_Variables.Position = creature_behaviour.transform.position;
            jSON_Variables.Rotation = creature_behaviour.transform.eulerAngles;
            string json = JsonUtility.ToJson(jSON_Variables);
            // Create the save directory if it doesn't already exist
            if (!Directory.Exists(SaveDirectoryBundle))
            {
                Directory.CreateDirectory(SaveDirectoryBundle);
            }
 
            // Save the JSON string to a file
            File.WriteAllText(SaveDirectoryBundle + fileName + i + ".json", json);
        }
    }
    public class JSON_Variables
    {
        public enum state
        {
            IDLE,
            CHASING,
            RUNNING,
            MULTIPLYING
        }
        public char[] Fitness;
        public string Name;
        public state State;
        public Vector3 Position;
        public Vector3 Rotation;
    }
}
