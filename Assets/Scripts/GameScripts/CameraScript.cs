using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera mainView;
    public  GameObject Stage;
    void Awake()
    {
        float height = Stage.transform.localScale.x;
        mainView.orthographicSize = height/2;
    }
}

