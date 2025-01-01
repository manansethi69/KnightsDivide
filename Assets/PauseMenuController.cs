using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Reference to the Pause Menu Panel
    private bool isPaused = false;

    void Update()
    {
        // Toggle pause menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure the game resumes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current level
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game resumes
        SceneManager.LoadScene("Simple Main Menu Demo"); // Replace "MainMenu" with your main menu scene name
    }
}

