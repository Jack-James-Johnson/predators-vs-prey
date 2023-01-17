using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject PauseUI;
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
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;

    }
}
