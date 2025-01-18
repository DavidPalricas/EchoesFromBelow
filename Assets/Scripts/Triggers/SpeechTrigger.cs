using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// The SpeechTrigger class is responsible for handling the logic of the a trigger that shows a speech box.
/// </summary>
public class SpeechTrigger : MonoBehaviour
{
    /// <summary>
    /// The speechBox property is responsible for storing the speech box game object.
    /// </summary>
    [SerializeField]
    private GameObject speechBox;

    /// <summary>
    /// The currentSpeechText property is responsible for storing the current speech text.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI currentSpeechText;

    /// <summary>
    /// The speechJsonFile property is responsible for storing the json file with the speech texts.
    /// </summary>
    [SerializeField]
    private TextAsset speechJsonFile;

    /// <summary>
    /// The playerActions property is responsible for storing the player's actions.
    /// </summary>
    [SerializeField]
    private PlayerActions playerActions;

    /// <summary>
    /// The speechTexts property is responsible for storing the speech texts.
    /// </summary>
    private Dictionary<string, string> speechTexts = new();

    /// <summary>
    /// The currentText property is responsible for storing the current text shown in the speech box.
    /// </summary>
    private string currentText = null;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are loading the speech texts from the json file.
    /// And getting the first text to show.
    /// </summary>
    private void Awake()
    {
        speechTexts  = JsonConvert.DeserializeObject<Dictionary<string, string>>(speechJsonFile.text);

        currentText = GetNextText();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the speech is active if it is, we are showing the speech by calling the ShowSpeech method.
    /// </summary>
    private void Update()
    {   
        if (Utils.isSpeechActive)
        {
            ShowSpeech();
        }
    }

    /// <summary>
    /// The ShowSpeech method is responsible for showing the speech in the speech box.
    /// If the player press the skip dialogue input, it gets the next text to show by calling the GetNextText method.
    /// </summary>
    private void ShowSpeech()
    {
        currentSpeechText.text = currentText;

        if (playerActions.InputTriggered("SkipDialogue"))
        {   
            currentText = GetNextText();
        }
    }

    /// <summary>
    /// The GetNextText method is responsible for getting the next text to show in the speech box.
    /// It removes the current text from the speech texts dictionary, unless is the first iteration and gets the value of the first key (next text).
    /// If there are no more texts to show, it sets the speech box inactive and destroys the game object.
    /// </summary>
    /// <returns>The the next text to show in the speech box or null if there is no more text to show</returns>
    private string GetNextText()
    {
        if (currentText != null)
        {
            speechTexts.Remove(speechTexts.Keys.First());
        }

        if (speechTexts.Count == 0)
        {
            Utils.isSpeechActive = false;
            speechBox.SetActive(Utils.isSpeechActive);
            Destroy(gameObject);
            return null;
        }

        return speechTexts[speechTexts.Keys.First()];
    }

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are checking if the player entered on trigger.
    /// If the player entered on trigger, we are setting the speech box active and informing the game that the speech is active (Utils.isSpeechActive).
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player"))
        {   
            Utils.isSpeechActive = true;
            speechBox.SetActive(Utils.isSpeechActive);
        }
    }
}
