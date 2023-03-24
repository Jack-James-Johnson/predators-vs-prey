using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public int SceneToLoad;
    public void Load()
    { // Loads scene from unity, with the scene number being passed through in the function call
        SceneManager.LoadScene(SceneToLoad);
    }
}
