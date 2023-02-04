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
    void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
   public void Resume()
    {
        SettingsUI.SetActive(false);
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
        gameManager.GetComponent<AudioSource>().pitch=1f;
        gameManager.GetComponent<AudioReverbFilter>().enabled=false;
        isGamePaused = false;
        Time.timeScale=currentSpeed;
        cameraControl.gameObject.SetActive(true);
        cameraControl.GetComponent<CameraScript>().enabled = true;
        gameManager.state = GameManager.stateType.ACTIVE;
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        GameUI.SetActive(false);
        gameManager.GetComponent<AudioSource>().pitch=0.75f;
        gameManager.GetComponent<AudioReverbFilter>().enabled=true;
        currentSpeed = Time.timeScale;
        Time.timeScale = 0f;
        isGamePaused = true;
        cameraControl.GetComponent<CameraScript>().enabled = false;
        gameManager.state = GameManager.stateType.PAUSED;

    }
    public void ReturnToMenu()
    {
        SceneLoader switcher = gameObject.AddComponent(typeof(SceneLoader)) as SceneLoader;
        switcher.SceneToLoad = 0; // Main Menu
        Resume();
        switcher.Load();
    }
}
