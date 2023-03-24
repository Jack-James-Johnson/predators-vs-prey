using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetSavedGames : MonoBehaviour
{
    public Button selectionPrefab;
    void Awake()
    {
        //when the script is spawned, call the getsaves function to get every available save
        getSaves();
    }
    public void getSaves()
    {
        //current directory is the save data directory created by unity
        var SaveDirectoryBundle = Application.persistentDataPath + $"/SaveData/";
        //for every save within the save folder
        foreach (string folder in Directory.GetDirectories(SaveDirectoryBundle))
        {
            //create a new button on the menu for current save,
            var button = Instantiate(selectionPrefab, selectionPrefab.transform);
            //under a pan-able group that scales with the amount of buttons 
            button.transform.SetParent(selectionPrefab.transform.parent, false);
            //name of the button is the name of the save
            button.name = Path.GetFileName(folder);
            //the text on the button is the name of the save
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = button.name;

        }
        //after all the buttons have been spawned, disable the prefab button
        selectionPrefab.gameObject.SetActive(false);
    }
}
