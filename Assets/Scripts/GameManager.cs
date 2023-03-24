using UnityEngine;
public class GameManager : MonoBehaviour
{

    public float AverageFitnessLevel = 50f;
    public float MutationIntensity = 20f;
    public float CreatureCap = 20f;
    public bool HighGraphics = true;
    public bool NewGame = true;
    public stateType state = stateType.MENU;
    public float MainVolume = 100f;
    public string folderToLoad;

    public static GameManager Instance;
    private void Awake()
    {
        //ensures only one instance of gameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
 
    public enum stateType
    {
        MENU,
        PAUSED,
        ACTIVE
    }
}