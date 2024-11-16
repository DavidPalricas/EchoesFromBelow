using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Level1Logic class is responsible for handling the logic of the first level.
/// </summary>
public class Level1Logic : MonoBehaviour
{
    /// <summary>
    /// The Keys property is responsible for storing the keys in the level and their values.
    /// If the value is true, the key is the correct key to open the gate, otherwise it is not.
    /// </summary>
    public Dictionary<GameObject, bool> Keys { get; private set; } = new Dictionary<GameObject, bool>();

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are retrieving the keys by calling the FindKeys() method and adding their values by calling the AddKeysValues() method.
    /// </summary>
    private void Awake()
    {
        GameObject[] gameKeys = FindKeys();

        AddKeysValues(gameKeys);


        foreach (var key in Keys)
        {
            Debug.Log(key.Key.name + " : " + key.Value);
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
}
