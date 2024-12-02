using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The TorchLightTrigger class is responsible for turning off the torch light.
/// </summary>
public class TorchLightTrigger : MonoBehaviour
{
    /// <summary>
    /// The isTorchLightOn variable is responsible for storing the state of the torch light.
    /// </summary>
    private bool isTorchLit, playerDetected;

    [SerializeField]
    private GameObject torchLit;

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

    private void Update()
    {
        if (!isTorchLit && playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            LitTorch();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
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
