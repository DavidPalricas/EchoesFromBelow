using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The MainMenu class is responsible for having the buttons logic in the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// The PlayGame method is responsible for loading the first level of the game.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// The ControlsMenu method is responsible for loading the controls menu.
    /// </summary>
    public void ControlsMenu()
    {
      SceneManager.LoadScene("ControlsMenu");
    }

    /// <summary>
    /// The QuitGame method is responsible for quitting the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
