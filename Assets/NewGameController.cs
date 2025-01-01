using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class NewGameController : MonoBehaviour
{
    public string sceneName;
    public void LoadScene1()
    {
        SceneManager.LoadScene(sceneName); 
    }
}

