using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The PlayerInputPreferences class is responsible for loading and applying the player's input preferences.
/// If the player did not change the input preferences, the default input preferences are applied.
/// </summary>
public class PlayerInputPreferences : MonoBehaviour
{
    /// <summary>
    /// The playerInput property is responsible for storing the player's input.
    /// It is serialized to be shown in the inspector.
    /// </summary>
    [SerializeField]
    private PlayerInput playerInput;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the player's input preferences are loaded and applied by calling the LoadAndApplyBindings method and the object is set to not be destroyed when loading a new scene, or in the pause menu.
    /// </summary>
    private void Awake()
    {
        Utils.LoadAndApplyBindings(playerInput);

        DontDestroyOnLoad(gameObject);
    }

}
