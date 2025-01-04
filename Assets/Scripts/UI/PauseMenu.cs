using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The PauseMenu class is responsible for handling the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The pauseMenuPanel property is responsible for storing the pause menu panel game object.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject pauseMenuPanel;

    /// <summary>
    /// The playerActions property is responsible for storing the player actions component.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private PlayerActions playerActions;

    /// <summary>
    /// The playerInput property is responsible for storing the player input component.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private PlayerInput playerInput;

    /// <summary>
    /// The gameIsPaused property is responsible for storing the game pause status.
    /// </summary>
    private bool gameIsPaused;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are setting the gameIsPaused variable to false and the pause menu panel to inactive.
    /// </summary>
    private void Awake()
    {
        gameIsPaused = false;
        pauseMenuPanel.SetActive(gameIsPaused);
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player pressed the pause/unpause input.
    /// If the player pressed the pause/unpause input, the HandlingPauseMenu method is called to handle the pause menu.
    /// </summary>
    private void Update()
    { 
        if (playerActions.InputTriggered("Pause/Unpause"))
        {
            HandlingPauseMenu();
        }
    }

    /// <summary>
    /// The handlingPauseMenu method is responsible for handling the pause menu.
    /// The game pause status is toggled (gameIsPaused porperty), and the pause menu panel is to active or inactive depending on the game pause status.
    /// The time is stopped when the game is paused and the HandlingInput method is called to handle the input (enable or disable the player input).
    /// </summary>
    private void HandlingPauseMenu()
    {   
        gameIsPaused = !gameIsPaused;

        pauseMenuPanel.SetActive(gameIsPaused);

        // Stops the time and everything in the game
        Time.timeScale = gameIsPaused ? 0 : 1;

        HandlingInput();
    }

    /// <summary>
    /// The HandlingInput method is responsible for handling the input.
    /// This method activates or deactivates the player input depending on the game pause status, if the game is paused the player input is disabled otherwise it is enabled.
    /// The only input action that is not disabled when the game is paused is the pause/unpause input.
    /// </summary>
    private void HandlingInput()
    {
        foreach (var action in playerInput.actions)
        {
            if (action.name != "Pause/Unpause")
            {
                if (gameIsPaused)
                {
                    action.Disable();
                }
                else
                {
                    action.Enable();
                }
            }
        }
    }
}
