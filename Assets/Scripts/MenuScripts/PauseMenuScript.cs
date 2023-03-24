using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;
    private float currentSpeed = 1;
    GameManager gameManager;
    public GameObject PauseUI;
    public GameObject cameraControl;
    public GameObject GameUI;
    public GameObject SettingsUI;
    public GameObject Spawner;
    void Awake()
    {
        //on start up, set the current instance of game manager to 
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        // if the input pressed by the user is the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if the game is currently paused, call resume function
            if (isGamePaused)
            {
                Resume();
            }
            //otherwise, call pause function
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        //disables every menu that isn't the game UI
        SettingsUI.SetActive(false);
        PauseUI.SetActive(false);
        //enables the game UI
        GameUI.SetActive(true);
        //change models of every creature to the current user setting
        Spawner.GetComponent<CreatureSpawner>().changeModels(gameManager.HighGraphics);
        //audio pitch is adjusted to default pitch and audio filter is removed from the music
        gameManager.GetComponent<AudioSource>().pitch = 1f;
        gameManager.GetComponent<AudioReverbFilter>().enabled = false;
        //game no longer paused, change variable
        isGamePaused = false;
        //reset time scale to the scale described in the slider for time scale
        Time.timeScale = currentSpeed;
        //allows camera movement
        cameraControl.gameObject.SetActive(true);
        cameraControl.GetComponent<CameraScript>().enabled = true;
        //game state set to active
        gameManager.state = GameManager.stateType.ACTIVE;
    }
    public void Pause()
    {
        //enables pause menu and disables game ui
        PauseUI.SetActive(true);
        GameUI.SetActive(false);
        //pitches down the game music and enables reverb game filter
        gameManager.GetComponent<AudioSource>().pitch = 0.75f;
        gameManager.GetComponent<AudioReverbFilter>().enabled = true;
        //sets the current speed variable of time to the slider value 
        //so that it can be re-enabled when the game is unpaused
        currentSpeed = Time.timeScale;
        //all movement is disabled
        Time.timeScale = 0f;
        //set game as paused disable camera movement 
        isGamePaused = true;
        cameraControl.GetComponent<CameraScript>().enabled = false;
        gameManager.state = GameManager.stateType.PAUSED;

    }
    public void ReturnToMenu()
    {
        //create object of scene loader, allowing the Load function to be used,
        //with the ability to change the attribute of SceneToLoad to be edited to main menu
        SceneLoader switcher = gameObject.AddComponent(typeof(SceneLoader)) as SceneLoader;
        switcher.SceneToLoad = 0; // Main Menu
        gameManager.NewGame = true;
        //resets time and sound settings before switching to main menu
        Resume();
        //changes scene to main menu
        switcher.Load();
    }
}