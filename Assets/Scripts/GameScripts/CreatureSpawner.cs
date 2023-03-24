using System;
using System.IO;
using System.Globalization;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject preyPrefab;
    public GameObject predatorPrefab;
    public bool IsPrey;
    public GameManager gameManager;
    public GameObject Stage;
    public GameObject spawner;
    public GameObject Timer;
    private float MaxHeight;
    private float MaxWidth;
    static string m_path;
    static string[] Adjectives;
    static string[] Nouns;
    public const string fileName = "creature";
    public void Start()
    {
        // Obtain max stage height and width
        MaxHeight = Stage.transform.localScale.x / 2;
        MaxWidth = Stage.transform.localScale.z / 2;
        //set the game manager variable as the current instance
        gameManager = GameManager.Instance;

        //m_path is set to the user's save data folder for the game, created by unity
        m_path = Application.dataPath;
        //set the adjective and noun lists as string arrays
        Adjectives = ((Resources.Load("Text/adjectives") as TextAsset).ToString()).Split(Environment.NewLine.ToCharArray());
        Nouns = ((Resources.Load("Text/nouns") as TextAsset).ToString()).Split(Environment.NewLine.ToCharArray());

        //if the scene was loaded into with NewGame as true
        if (gameManager.NewGame)
        {
            //start a new random summon
            InitSummon();
            gameManager.NewGame = false;
        }
        else
        {
            //load the creatures passed through from the load game option
            LoadCreatures();
        }

        //change the models to the current value of gameManager.HighGraphics bool
        changeModels(gameManager.HighGraphics);
        //disable the prefab objects active boolean
        preyPrefab.SetActive(false);
        predatorPrefab.SetActive(false);
    }
    public void InitSummon()
    { 
        //spawn CreatureCap amount of creatures
        for (int _ = 0; _ < gameManager.CreatureCap; _++)
        {
            //set the spawn position of the current creature as a random position within the stage bounds
            Vector3 SpawnPosition = new Vector3(UnityEngine.Random.Range(-MaxHeight, MaxHeight), 1, UnityEngine.Random.Range(-MaxWidth, MaxWidth));
            // if the creature is in the first half of the numbers to iterate through, toSpawn is prey,
            // otherwise to spawn is a predator
            GameObject toSpawn = (_ > gameManager.CreatureCap / 2 ? preyPrefab : predatorPrefab);
            // instantiate a creature with the position and role set previously
            var creature = Instantiate(toSpawn, SpawnPosition, Quaternion.identity, this.transform);
            // randomly generate creatureName 
            creature.name = generateString();
        }
    }
    public void changeModels(bool highGraphics)
    {
        // for each creature that has been spawned 
        foreach (Transform creature in spawner.transform)
        {
            //set the first child (high graphic model) active is highGraphics == true
            creature.GetChild(0).gameObject.SetActive(highGraphics);
            // set the other child (low graphic model) active if highGraphics == false 
            creature.GetChild(1).gameObject.SetActive(!highGraphics);
        }
    }
    public void LoadCreatures()
    {
        //loads the folder containing the desired save to be loaded
        var SaveDirectoryBundle = Application.persistentDataPath + $"/SaveData/{gameManager.folderToLoad}/";
        string jsonConditions = File.ReadAllText(SaveDirectoryBundle + "game_conditions.json");
        
        //deserialize conditions from jsonConditions file into Game_conditions object 
        Game_Conditions conditions = JsonUtility.FromJson<Game_Conditions>(jsonConditions);

        //set the gameManager global variables to currentGame conditions as determined by file
        gameManager.AverageFitnessLevel = conditions.AverageFitness;
        gameManager.CreatureCap = conditions.CreatureCap;
        gameManager.MutationIntensity = conditions.MutationIntensity;
        //set the game's current time to be the timer in the conditions file
        Timer.GetComponent<TimerScript>().SetTime(conditions.Time);

        foreach (string file in Directory.GetFiles(SaveDirectoryBundle))
        {
            //for every file within the save folder that isn't the game conditions 
            if (file != SaveDirectoryBundle+"game_conditions.json")
            {
                string text = File.ReadAllText(file); 
                //deserialize the text into the JSON_Variables game object
                JSON_Variables creature = JsonUtility.FromJson<JSON_Variables>(text);
                //check the isPrey var, if true, then toInstantiate is prey, otherwise predator
                GameObject toInstantiate = creature.IsPrey ? preyPrefab : predatorPrefab;
                //instantiate the creature with the properties of the creature that has been deserialized
                var currentCreature = Instantiate(toInstantiate, creature.Position, Quaternion.Euler(creature.Rotation), spawner.transform);
                currentCreature.name = creature.Name;
                //generate the same stats with the fitness value described in the save
                currentCreature.GetComponent<CreatureBehaviour>().GenerateStats(new string(creature.Fitness));
            }
        }
    }
    private string generateString()
    {
        //allows TitleCase function to format the names
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        //picks random noun and adjective from their respective lists, and converts to title case
        string adjective = textInfo.ToTitleCase(Adjectives[UnityEngine.Random.Range(0, Adjectives.Length)]);
        string noun = textInfo.ToTitleCase(Nouns[UnityEngine.Random.Range(0, Nouns.Length)]);
        // concatenate adjective and noun
        return ($"{adjective} {noun}");
    }
    public void SaveGame()
    {
        // save current Unix Time 
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        //converts to int amount of seconds
        int secondsSinceEpoch = (int)t.TotalSeconds;
        //creates a new folder within the save location with the amount of seconds as the name
        var SaveDirectoryBundle = Application.persistentDataPath + $"/SaveData/{secondsSinceEpoch}/";
        //for amount of children within the spawner
        for (int i = 0; i < transform.childCount; i++)
        {
            //index current child from children in spawner
            Transform child = transform.GetChild(i);
            //create a new JSON_Variables object
            JSON_Variables jSON_Variables = new JSON_Variables();
            //if child has a PreyBehaviour function (therefore is prey)
            if (child.TryGetComponent<PreyBehaviour>(out var prey_behaviour))
            {
                //set all of the current properties of prey to Object
                jSON_Variables.IsPrey = true;
                jSON_Variables.Fitness = prey_behaviour.fitness;
                jSON_Variables.Name = prey_behaviour.name;
                jSON_Variables.State = prey_behaviour.currentState;
                jSON_Variables.Position = prey_behaviour.transform.position;
                jSON_Variables.Rotation = prey_behaviour.transform.eulerAngles;
            }
            else
            {
                //set all the current properties of predator to Object
                var predator_behaviour = child.GetComponent<PredatorBehaviour>();
                jSON_Variables.IsPrey = true;
                jSON_Variables.Fitness = predator_behaviour.fitness;
                jSON_Variables.Name = predator_behaviour.name;
                jSON_Variables.State = predator_behaviour.currentState;
                jSON_Variables.Position = predator_behaviour.transform.position;
                jSON_Variables.Rotation = predator_behaviour.transform.eulerAngles;
            }
            //serializes the object as JSON file
            string json = JsonUtility.ToJson(jSON_Variables);
            // Create the save directory if it doesn't already exist
            if (!Directory.Exists(SaveDirectoryBundle))
            {
                Directory.CreateDirectory(SaveDirectoryBundle);
            }

            // Save the JSON string to a file
            File.WriteAllText(SaveDirectoryBundle + fileName + i + ".json", json);
        }
        //creates a new Game_conditions object
        Game_Conditions game_conditions = new Game_Conditions();
        //saves the current time of game session
        game_conditions.Time = Timer.GetComponent<TimerScript>().currentTime;
        //saves all the conditions of the game session
        game_conditions.AverageFitness = gameManager.AverageFitnessLevel;
        game_conditions.CreatureCap = gameManager.CreatureCap;
        game_conditions.MutationIntensity = gameManager.MutationIntensity;
        //convert previous game_conditions attributes to a JSON
        string gameConditions = JsonUtility.ToJson(game_conditions);
        //write JSON to a file
        File.WriteAllText(SaveDirectoryBundle + "game_conditions.json", gameConditions);
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
        public bool IsPrey;
    }
    public class Game_Conditions
    {
        public float Time;
        public float CreatureCap;
        public float AverageFitness;
        public float MutationIntensity;
    }
}