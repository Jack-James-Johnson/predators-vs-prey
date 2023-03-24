using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TMP_Text timerText;
    public float currentTime;
    public float startTime;
    private GameManager gameManager;
    void Start()
    {
        //sets game manager to current instance
        gameManager = GameManager.Instance;
        //if the game has been initialized with new game as true, set the start time as current time
        if(gameManager.NewGame){
            startTime = Time.time;
        }
    }
    void Update()
    {
        //time elapsed on screen is the current current time - initial time
        currentTime = Time.time - startTime;

        //various math to convert seconds into whole hours, minutes, and seconds
        string hours = ((int)currentTime / 3600).ToString();
        string minutes = (((int)currentTime % 3600) / 60).ToString("D2");
        string seconds = (currentTime % 60).ToString("f2");
        timerText.text = ($"{hours}:{minutes}:{seconds}");
    }
    public void SetTime(float time)
    {
        startTime = Time.time - time;
    }
}
