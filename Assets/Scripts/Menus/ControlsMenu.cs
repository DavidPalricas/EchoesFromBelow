using UnityEngine;

/// <summary>
/// The ControlsMenu class is responsible for having the buttons logic in the controls menu.
/// </summary>
public class ControlsMenu : MonoBehaviour
{
    [SerializeField]
    private RebindingMenu rebindingMenu;
    
    /// <summary>
    /// The KeyBoardBindings method is responsible for loading the key board bindings menu.
    /// </summary>
    public void KeyBoardBindings()
    {   
        rebindingMenu.UpdateUIText(0);
    }

    /// <summary>
    /// The GamePadBindings method is responsible for loading the game pad bindings menu.
    /// </summary>
    public void GamePadBindings()
    {
        rebindingMenu.UpdateUIText(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
