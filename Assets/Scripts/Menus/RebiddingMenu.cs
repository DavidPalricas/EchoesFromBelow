using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// The RebindingMenu class is responsible for handling the rebinding menu logic.
/// </summary>
public class RebindingMenu : MonoBehaviour
{
    /// <summary>
    /// The playerInput property is responsible for storing the player input.
    /// </summary>
    [SerializeField]
    private PlayerInput playerInput;

    /// <summary>
    /// The attackButton, healButton, interactButton, pauseButton, switchWeaponsButton, skipDialogueButton properties are responsible for storing the buttons of the actions that can be rebound.
    /// </summary>
    [SerializeField]
    private GameObject attackButton, healButton, interactButton, pauseButton, switchWeaponsButton, skipDialogueButton;

    /// <summary>
    /// The defaultButtonImageColor property is responsible for storing the default color of the button image.
    /// </summary>
    private static readonly Color defaultButtonImageColor = Color.black;

    /// <summary>
    /// The defaultButtonTextColor property is responsible for storing the default color of the button text.
    /// </summary>
    private static readonly Color defaultButtonTextColor = Color.white;

    /// <summary>
    /// The action index property is responsible for storing the index of the action.
    /// 0 for keyboard and 1 for gamepad.
    /// </summary>
    private int actionIndex;

    /// <summary>
    /// The SaveBindings method is responsible for saving the bindings.
    /// The new bindings are saved in the player preferences , this preferences are store in the PlayerPrefs class (Unity class).
    /// The PlayerPrefs class is used to save and load player preferences between game sessions.
    /// </summary>
    private void SaveBindings()
    {
        string rebinds = playerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString("inputBindings", rebinds);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// The WaitingForRebind method is responsible for waiting for the rebind of the action to complete this method is called when the player inserts a key or button to rebind the action.
    /// The rebind operation is performed interactively and the controls are excluded based on the action index.
    /// If the action is for keyboard/mouse (value 0), the gamepad controls are excluded and vice versa.
    /// After the rebind operation is completed, the RebindingComplete method is called and this operation is disposed.
    /// </summary>
    /// <param name="actionToRebind">The action to rebind.</param>
    /// <param name="actionButton">The button of the action to rebind.</param>
    private void WaitingForRebind(InputActionReference actionToRebind, GameObject actionButton)
    {
        var rebindOperation = actionToRebind.action.PerformInteractiveRebinding();

        if (actionIndex == 0)
        {
            rebindOperation = rebindOperation.WithControlsExcluding("<Gamepad>");
        }
        else
        {
            rebindOperation = rebindOperation.WithControlsExcluding("<Keyboard>");
        }

        rebindOperation
            .WithTargetBinding(actionIndex)
            .WithControlsExcluding("<Mouse>")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                RebindingComplete(actionToRebind, actionButton);
                operation.Dispose();
            })
            .Start();
    }

    /// <summary>
    /// The RebindingComplete method is called when the rebind operation is completed.
    /// In this method, we are updating the action button text with the new binding path.
    /// Before applying the new binding, we are checking if the new binding is not a duplicated.
    /// If the new binding is duplicated, the action button colors (image and text) are changed to inform  the player that the key is already in use and we are removing the binding override (reverting the changes).
    /// Otherwise, the new binding is applied and saved.
    /// </summary>
    /// <param name="actionToRebind">The action to rebind.</param>
    /// <param name="actionButton">The action button.</param>
    private void RebindingComplete(InputActionReference actionToRebind, GameObject actionButton)
    {
        string newBindingPath = actionToRebind.action.bindings[actionIndex].effectivePath;

        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
            newBindingPath,
            InputControlPath.HumanReadableStringOptions.OmitDevice).ToUpper();

        actionToRebind.action.ApplyBindingOverride(
            actionIndex,
            newBindingPath);

        if (CheckForDuplicateBinding(newBindingPath) && actionButton.TryGetComponent(out UnityEngine.UI.Image actionButtonImage))
        {
                // Dark red color
                actionButtonImage.color = new Color(0.5f, 0f, 0f, 1f);

                actionButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

                actionToRebind.action.RemoveBindingOverride(actionIndex);

                return;
        }

        SaveBindings();
    }

    /// <summary>
    /// The GetActionToRebind method is responsible for getting the action to rebind based on the button clicked by the player.
    /// </summary>
    /// <param name="button">The button clicked by the player.</param>
    /// <returns></returns>
    private InputAction GetActionToRebind(GameObject button)
    {
        return button switch
        {
            _ when button == attackButton => playerInput.actions["Attack"],
            _ when button == healButton => playerInput.actions["Heal"],
            _ when button == interactButton => playerInput.actions["Interact"],
            _ when button == pauseButton => playerInput.actions["PauseUnpause"],
            _ when button == switchWeaponsButton => playerInput.actions["SwitchWeapons"],
            _ when button == skipDialogueButton => playerInput.actions["SkipDialogue"],
            _ => null
        };
    }

    /// <summary>
    /// The CheckForDuplicateBinding method is responsible for checking if the new binding path is a duplicate key.
    /// To check this a counter is used to count the number of duplicate keys by iterating through the actions and comparing the bindings paths.
    /// If the action is Movement, we are iterating through the bindings of the action and comparing the paths, because the Movement action has multiple bindings.
    /// </summary>
    /// <param name="newBindingPath">The path of the new binding.</param>
    /// <returns>
    /// <c>true</c> if the new binding is duplicate ; otherwise, <c>false</c>.
    /// </returns>
    private bool CheckForDuplicateBinding(string newBindingPath)
    {
        int duplicateCount = 0;

        foreach (InputAction action in playerInput.actions)
        {
            if (action.name != "Movement")
            {
                if (action.bindings[actionIndex].effectivePath == newBindingPath)
                {
                    if (duplicateCount == 1)
                    {
                        return true;
                    }

                    duplicateCount++;
                }
            }
            else
            {
                foreach (InputBinding binding in action.bindings)
                {
                    if (binding.effectivePath == newBindingPath)
                    {
                        if (duplicateCount == 1)
                        {
                            return true;
                        }

                        duplicateCount++;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// The StartRebinding method is responsible for starting the rebind operation after the player clicks the action button to rebind.
    /// The user is informed to press a key to rebind the action.
    /// After that the action to rebind is found by calling the GetActionToRebind method and the WaitingForRebind method is called for waiting to the user's input.
    /// </summary>
    public void StartRebinding()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.GetComponentInChildren<TextMeshProUGUI>().text = actionIndex == 0 ? "Press a key".ToUpper() : "Press a button".ToUpper();

        // Reset the button colors to default because the player could be rebinding an action with a duplicated binding
        button.GetComponentInChildren<TextMeshProUGUI>().color = defaultButtonTextColor;
        button.GetComponent<UnityEngine.UI.Image>().color = defaultButtonImageColor;

        InputAction actionToRebind = GetActionToRebind(button);

        if (actionToRebind == null)
        {
            Debug.LogError("Action not found");
            return;
        }

        WaitingForRebind(InputActionReference.Create(actionToRebind), button);
    }

    /// <summary>
    /// The UpdateUIText method is responsible for updating the UI text (action buttons text).
    /// To update the action buttons text, the action bindings are loaded in order to get the key or button that is binded to the action.
    /// </summary>
    public void UpdateUIText(int newActionIndex)
    {   

        Utils.LoadAndApplyBindings(playerInput);

        actionIndex = newActionIndex;

        Dictionary<GameObject, string> actions = new()
        {
            {attackButton, "Attack" },
            { healButton, "Heal" },
            { interactButton, "Interact" },
            { pauseButton, "PauseUnpause" },
            { switchWeaponsButton, "SwitchWeapons" },
            { skipDialogueButton, "SkipDialogue"}
        };

        Debug.Log(actionIndex);

        foreach (KeyValuePair<GameObject, string> action in actions)
        {  
            GameObject actionButton = action.Key;

            actionButton.GetComponentInChildren<TextMeshProUGUI>().text = InputControlPath.ToHumanReadableString(
                playerInput.actions[action.Value].bindings[actionIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice).ToUpper();

            actionButton.GetComponentInChildren<TextMeshProUGUI>().color = defaultButtonTextColor;
            actionButton.GetComponent<UnityEngine.UI.Image>().color = defaultButtonImageColor;
        }
    }

    /// <summary>
    /// The ResetDefaultBindings method is responsible for resetting the default bindings if the player clicks the reset button.
    /// After resetting the default bindings, the player preferences are cleared and the UI text is updated.
    /// </summary>
    public void ResetDefaultBindings()
    {
        foreach (InputAction action in playerInput.actions)
        {
            action.RemoveBindingOverride(actionIndex);
        }

        PlayerPrefs.SetString("inputBindings", string.Empty);

        UpdateUIText(actionIndex);
    }
}
