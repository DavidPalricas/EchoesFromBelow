using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The CameraMovement class is responsible for handling the camera's movement.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's GameObject.
    /// </summary>
    private GameObject player;

    private int currentSceneIndex;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the player variable is initialized.
    /// </summary>
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume") / 10;
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the camera follows the player.
    /// </summary>
    private void Update()
    {
        if (currentSceneIndex != 0)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
