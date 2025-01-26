using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The Level1Logic class is responsible for handling the logic of the first level.
/// </summary>
public class Level1Logic : MonoBehaviour
{
    /// <summary>
    /// The playerRank property is responsible for storing the player's rank and its status.
    /// </summary>
    [SerializeField]
    private Rank playerRank;

    /// <summary>
    /// The speech trigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The enemyTutorialWall , player properties are responsible for storing the enemy tutorial wall and the player game objects.
    /// They are serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject enemyTutorialWall, player;

    /// <summary>
    /// The SKELETONSTUTORIAL property is responsible for storing the number of skeletons in the combat tutorial.
    /// </summary>
    private const int SKELETONSTUTORIAL = 5;

    /// <summary>
    /// The Keys property is responsible for storing the keys in the level and their values.
    /// If the value is true, the key is the correct key to open the gate, otherwise it is not.
    /// </summary>
    public Dictionary<GameObject, bool> Keys { get; private set; } = new Dictionary<GameObject, bool>();

    /// <summary>
    /// The firstTorchLit property is responsible for checking if the player has lit the first torch.
    /// It is HiddenInInspector because it is only used to check if the first torch was lit.
    /// </summary>
    [HideInInspector]
    public bool firstTorchLit = false;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are retrieving the keys by calling the FindKeys() method and adding their values by calling the AddKeysValues() method.
    /// </summary>
    private void OnEnable()
    {
        AddKeysValues(FindKeys());
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player has killed the skeletons tutorial and if the enemy tutorial wall is destroyed.
    /// </summary>
    private void Update()
    {
        if (playerRank.EnemiesKilled == SKELETONSTUTORIAL && !enemyTutorialWall.IsDestroyed())
        {
            speechTrigger.ChangeSpeech("skeletonsTutorialSpeech");
            Destroy(enemyTutorialWall);
        }
    }

    /// <summary>
    /// The FindKeys method is responsible for finding the keys in the level.
    /// If there are not enough keys or too many keys in the level, an error message will be displayed.
    /// The correct number of keys is 4.
    /// </summary>
    /// <returns> An array with the keys of the level</returns>
    private GameObject[] FindKeys()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        // The keyIndex is set to -1 , because in debug erros the number of keys is shown in the keyIndex + 1 format
        int keyIndex = -1;

        GameObject[] keys = new GameObject[4];

        foreach (var item in items)
        {
            if (item.name.Contains("Key"))
            {   
                if (keyIndex == 3)
                {
                    Debug.LogError("There are too many keys in the level (current keys : " + (keyIndex + 1) + " expected keys: 4");
                    return null;
                }

                keyIndex++;
                keys[keyIndex] = item;
            }
        }

        if (keyIndex == 3)
        {
            return keys;
        }

        Debug.LogError("There are not enough keys in the level (current keys : " + (keyIndex + 1) + " expected keys: 4");

        return null; 
    }

    /// <summary>
    /// The AddKeysValues method is responsible for adding the keys and their values to the Keys dictionary.
    /// The key values are created by calling the CreateKeyValues() method.
    /// </summary>
    /// <param name="gameKeys"> An array with the keys founded in the first level</param>
    private void AddKeysValues(GameObject[] gameKeys)
    {
        bool[] keys_values = CreateKeyValues();

        for (int i = 0; i < 4; i++)
        {
            Keys.Add(gameKeys[i], keys_values[i]);
        }
    }

    /// <summary>
    /// The CreateKeyValues method is responsible for creating the key values.
    /// This values are stored in an array of booleans.
    /// The index of the true value is randomly generated, and the other values are false.
    /// The true value corresponds to the correct key to open the gate.
    /// </summary>
    /// <returns> A boolean array with the key's values</returns>
    private bool[] CreateKeyValues()
    {
        // Creates a random number between 0 and 3
        int true_value_index = Random.Range(0, 4);

        var keys_values = new bool[4];

        for (int i = 0; i < keys_values.Length; i++)
        {
            keys_values[i] = i == true_value_index;
        }

        return keys_values;
    }

    /// <summary>
    /// The FirstTorchLitSpeech method is responsible for showing the first torch lit speech.
    /// </summary>
    public void FirstTorchLitSpeech()
    {
        firstTorchLit = true;
        speechTrigger.ChangeSpeech("firstLitTorchSpeech");
    }

    /// <summary>
    /// The BossKilled method is responsible for setting the player rank to BossKilled and showing the boss killed speech.
    /// </summary>
    public void BossKilled()
    {
        playerRank.BossKilled = true;
        speechTrigger.ChangeSpeech("bossKilledSpeech");
    }
}
