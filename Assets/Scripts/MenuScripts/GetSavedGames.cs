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
        getSaves();
    }
    public void getSaves()
    {
        var SaveDirectoryBundle = Application.persistentDataPath +  $"/SaveData/";
        foreach(string folder in Directory.GetDirectories(SaveDirectoryBundle))
        {
            Debug.Log(Path.GetFileName(folder));
            var button = Instantiate(selectionPrefab,selectionPrefab.transform);
            //button.GetComponent<TextMeshPro>().text = Path.GetFileName(folder);
        }
    }
}
