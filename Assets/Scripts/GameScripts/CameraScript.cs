using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraScript : MonoBehaviour
{
    public Camera mainView;
    public Canvas canvas;
    public GameObject Stage;
    private float height;
    private Vector3 position;
    [SerializeField]
    private GameObject trackObject;
    void Awake()
    {
        height = Stage.transform.localScale.x;
        
        mainView.orthographicSize = height / 2;
    }
    void Update()
    {
        //if an object is being tracked
        if(trackObject!=null){
            //every frame. make camera position the above tracked creature
            mainView.transform.position = new Vector3(trackObject.transform.position.x,10,trackObject.transform.position.z);
        }
        else{
                //if RMB has been clicked. save location
                if (Input.GetMouseButtonDown(1))
                {
                    position = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
                }
                //while RMB is being held, pan camera according to movement
                if (Input.GetMouseButton(1))
                {
                    Vector3 newPosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
                    mainView.transform.position += (position - newPosition) * mainView.orthographicSize / 500;
                    position = newPosition;
                }
        }
        // If desired zoom is both less than the height of stage, and greater than 1
        if (mainView.orthographicSize  - Input.mouseScrollDelta.y <= height / 2 && mainView.orthographicSize  - Input.mouseScrollDelta.y >= 1)
        {
            mainView.orthographicSize -= Input.mouseScrollDelta.y;
        }
        else if (mainView.orthographicSize  - Input.mouseScrollDelta.y > height / 2)
        {
            // If the desired zoom is greater than the stage size, change to max size
            mainView.orthographicSize = height / 2;
        }
        else
        {
            // If the desired zoom is smaller than what is capable, change to smallest size
            mainView.orthographicSize = 1;
        }
      
        // If LMB is clicked
        if(Input.GetMouseButtonDown(0))
        { 
            // Get position of mouse
            position = Input.mousePosition;
            // Cast a ray to the mouse position
            Ray ray = mainView.ScreenPointToRay(position);
            // If the ray hits a creature
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                // Set "trackObject" to the creature clicked
                trackObject = hit.transform.gameObject;
                trackObject.GetComponent("Creature Behaviour");
                // Call the change text function to display attributes of creature
                canvas.GetComponent<DisplayCreatureValues>().ChangeText(trackObject);
            }
            else
            {
                // If nothing is clicked, then remove the current creature and hide text
                trackObject = null;
                canvas.GetComponent<DisplayCreatureValues>().HideText();
            }

        }
    }
}