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
    private bool isTorchLightOn, playerDetected;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// Here, the isTorchLightOn variable is set to true.
    /// </summary>
    private void Awake()
    {
        isTorchLightOn = true;
    }

    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            TunrOnOffLight();
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
    /// The TunrOnOffLight method is responsible for turning on or off the torch light.
    /// To turn on or off the torch light, a timer of 0.5 seconds is used.
    /// </summary>
    private void TunrOnOffLight()
    {
        isTorchLightOn = !isTorchLightOn;

        if (isTorchLightOn)
        {
            Debug.Log("Torch light on");
        }
        else
        {
            Debug.Log("Torch light off");
        }
    }
}
