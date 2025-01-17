using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The ControlsMenu class is responsible for having the buttons logic in the controls menu.
/// </summary>
public class ControlsMenu : MonoBehaviour
{
    [SerializeField]
    private RebindingMenu rebindingMenu;

    private GameObject dontDestroyOnLoad;
    
    /// <summary>
    /// The KeyBoardBindings method is responsible for loading the key board bindings menu.
    /// </summary>
    public void KeyBoardBindings()
    {
        rebindingMenu.actionIndex = 0;
        rebindingMenu.UpdateUIText();
    }

    /// <summary>
    /// The GamePadBindings method is responsible for loading the game pad bindings menu.
    /// </summary>
    public void GamePadBindings()
    {
        rebindingMenu.actionIndex = 1;
        rebindingMenu.UpdateUIText();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
