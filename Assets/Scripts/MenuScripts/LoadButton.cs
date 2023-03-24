using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    public GameManager gameManager;
    public void setLoadVariable()
    {
        //gets the current instance of game manager
        gameManager = GameManager.Instance;
        //set the folder to load as the save name
        gameManager.folderToLoad = this.name;
        // set the new game variable as false, because a game is being loaded
        gameManager.NewGame = false;
    }
}
