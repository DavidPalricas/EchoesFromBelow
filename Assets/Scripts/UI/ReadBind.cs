using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The ReadBind class is responsible for reading the input from the player and displaying it on the screen.
/// </summary>
public class ReadBind : MonoBehaviour
{
    /// <summary>
    /// The bindTextUI property is responsible for storing the text to show the input's bind in the UI.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI bindTextUI;

    /// <summary>
    /// The bindText property is responsible for storing the text to show the input's bind in texts of the game.
    /// </summary>
    [SerializeField]
    private TextMeshPro bindText;

    /// <summary>
    /// The inputAction property is responsible for storing the input action reference.
    /// </summary>
    [SerializeField]
    private InputActionReference inputAction;

    /// <summary>
    /// The currentDevice property is responsible for storing the current input device.
    /// </summary>
    private InputDevice currentDevice;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are setting the current device and updating the bind text.
    /// </summary>
    private void Awake()
    {
        currentDevice = GetCurrentDevice();
        UpdateBindText();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the current device is different from the active input device.
    /// If it is, we are updating the current device and the bind text.
    /// </summary>
    private void Update()
    {
        InputDevice newCurrentDevice = GetCurrentDevice();

        if (newCurrentDevice != null && currentDevice != newCurrentDevice)
        {
            currentDevice = newCurrentDevice;
            UpdateBindText();
        }
    }

    /// <summary>
    /// The GetCurrentDevice method is responsible for getting the current input device (keyboard or Gamepad).
    /// If the current device is not changed, it returns null.
    /// </summary>
    /// <returns>The new current input device (keyboard or Gamepad) or null if the input sistem is not changed</returns>
    private InputDevice GetCurrentDevice()
    {   
        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
        {
            return Keyboard.current;
        }
        else if (Gamepad.current != null && Gamepad.current.allControls.Any(control => control.IsPressed()))
        {
            return Gamepad.current;
        }

        return null;
    }

    /// <summary>
    /// The UpdateBindText method is responsible for updating the bind text, based on the current device.
    // It shows the text in the UI or in the game texts.
    /// </summary>
    private void UpdateBindText()
    {   
        string bindPath  = inputAction.action.bindings[0].effectivePath;

        if (currentDevice is Gamepad)
        {
            bindPath = inputAction.action.bindings[1].effectivePath;
        }
        
        string bindMessage = "Press " + InputControlPath.ToHumanReadableString(bindPath, InputControlPath.HumanReadableStringOptions.OmitDevice).ToUpper();

        if (bindTextUI != null)
        {
            bindTextUI.text = bindMessage;
        }
        else
        {
            bindText.text = bindMessage;
        }
    }
}
