using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public int SceneToLoad;
    public void Load()
    { // Loads 
        SceneManager.LoadScene(SceneToLoad);
    }
}
