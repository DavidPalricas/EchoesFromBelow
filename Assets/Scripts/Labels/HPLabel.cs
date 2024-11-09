using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// The HPLabel class is responsible for showing the player's health on the screen.
/// </summary>
public class HPLabel : MonoBehaviour
{
    /// <summary>
    /// The labelText property is responsible for storing the text which will display the player's health on the screen.
    /// </summary>
    private TMP_Text labelText;

    /// <summary>
    /// The player property is responsible for storing the player's game object.
    /// </summary>
    private GameObject player;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method)
    /// In this method, the player property and labelText property are initialized.
    /// </summary>
    private void Awake()
    {    
        labelText = GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method,we are checking if the player's health is less than or equal to 0, if it is, the GameOver method is called.
    /// Otherwise, the UpdateHPLabel method is called.
    /// </summary>
    void Update()
    {   
        if (player.GetComponent<Entity>().Health <= 0)
        {
            GameOver();
        }
        else
        {
           UpdateHPLabel();
        }
    }

    /// <summary>
    /// The GameOver method is responsible for showing the player's health as 0 and loading the GameOver scene.
    /// </summary>
    private void GameOver()
    {
        labelText.text = "HP: " + 0;
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// The UpdateHPLabel method is responsible for updating the player's health on the screen.
    /// </summary>
    private void UpdateHPLabel()
    {
        labelText.text = "HP: " + player.GetComponent<Entity>().Health;
    }
}
