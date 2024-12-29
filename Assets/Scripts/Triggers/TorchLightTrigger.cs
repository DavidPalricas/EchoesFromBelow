using UnityEngine;

/// <summary>
/// The TorchLightTrigger class is responsible for turning off the torch light.
/// </summary>
public class TorchLightTrigger : MonoBehaviour
{
    /// <summary>
    /// The playerDetected and isTorchLit variables are responsible for storing the player detection and torch light status, respectively.
    /// </summary>
    private bool isTorchLit, playerDetected;

    /// <summary>
    /// The torchLit property is responsible for storing the torch with light game object.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject torchLit;

    /// <summary>
    /// The torchUnlit property is responsible for storing the torch without light game object.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject torchUnlit;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// Here, the isTorchLightOn variable is set to true.
    /// </summary>
    private void Awake()
    {
        isTorchLit = false;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is detected and if the player pressed the E key.
    /// Íf these conditions are met, the LitTorch method is called to lit the torch.
    /// </summary>
    private void Update()
    {
        if (!isTorchLit && playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            LitTorch();
        }
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method, we are setting the playerDetected variable to true (player enter the torch area).
    /// </summary>
    /// <param name="collision">The collider of the game object that collided..</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are setting the playerDetected variable to false (player left the torch area).
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    /// <summary>
    /// The LitTorch method is responsible for lit a torch
    /// </summary>
    private void LitTorch()
    {   
        isTorchLit= true;

        torchLit.SetActive(true);

        torchUnlit.SetActive(false);
    }
}
