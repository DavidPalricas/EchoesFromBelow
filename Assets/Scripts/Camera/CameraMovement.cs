using UnityEngine;

/// <summary>
/// The CameraMovement class is responsible for handling the camera's movement.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's GameObject.
    /// </summary>
    private GameObject player;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the player variable is initialized.
    /// </summary>
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the camera follows the player.
    /// </summary>
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
