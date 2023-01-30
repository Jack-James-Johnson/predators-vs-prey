using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int SceneToLoad;
    public void Load()
    { // Loads 
        SceneManager.LoadScene(SceneToLoad);
    }
}
