using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class toMainMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            SceneManager.LoadScene("Simple Main Menu Demo");
        }
    }
}
