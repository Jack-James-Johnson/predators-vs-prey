using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera mainView;
    public GameObject Stage;
    private float height;
    private Vector3 position;
    void Awake()
    {
        height = Stage.transform.localScale.x;
        mainView.orthographicSize = height / 2;
    }
    void Update()
    {
        //if both the size is less than 
        if (mainView.orthographicSize  - Input.mouseScrollDelta.y <= height / 2 && mainView.orthographicSize  - Input.mouseScrollDelta.y >= 1)
        {
            mainView.orthographicSize -= Input.mouseScrollDelta.y;
        }
        else if (mainView.orthographicSize  - Input.mouseScrollDelta.y > height / 2)
        {
            mainView.orthographicSize = height / 2;
        }
        else
        {
            mainView.orthographicSize = 1;
        }
        if (Input.GetMouseButtonDown(0))
        {
            position = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
            mainView.transform.position += (position - newPosition) * mainView.orthographicSize / 500;
            position = newPosition;
        }
    }
}


