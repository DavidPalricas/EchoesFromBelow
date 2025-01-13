using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The ControlsMenu class is responsible for having the buttons logic in the controls menu.
/// </summary>
public class ControlsMenu : MonoBehaviour
{
    /// <summary>
    /// The KeyBoardBindings method is responsible for loading the key board bindings menu.
    /// </summary>
    public void KeyBoardBindings()
    {
        Utils.IsKeyBoardBinding = true;
        SceneManager.LoadScene("BindingsMenu");
    }

    /// <summary>
    /// The GamePadBindings method is responsible for loading the game pad bindings menu.
    /// </summary>
    public void GamePadBindings()
    {
        Utils.IsKeyBoardBinding = false;
        SceneManager.LoadScene("BindingsMenu");
    }

    /// <summary>
    /// The ReturnMainMenu method is responsible for returning to the main menu.
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
