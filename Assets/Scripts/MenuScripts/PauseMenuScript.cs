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
        PauseUI.SetActive(false);
        isGamePaused = false;
        Time.timeScale=currentSpeed;
        cameraControl.gameObject.SetActive(true);
        cameraControl.GetComponent<CameraScript>().enabled = true;
        gameManager.state = GameManager.stateType.ACTIVE;
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        currentSpeed = Time.timeScale;
        Time.timeScale = 0f;
        isGamePaused = true;
        cameraControl.GetComponent<CameraScript>().enabled = false;
        gameManager.state = GameManager.stateType.PAUSED;

    }
}
