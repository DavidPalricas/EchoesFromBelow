using UnityEngine;

/// <summary>
/// The TorchLightTrigger class is responsible for turning off the torch light.
/// </summary>
public class TorchLightTrigger : BaseInteractionTrigger
{
    /// <summary>
    /// The isTorchLit property is responsible for storing the torch light status.
    /// </summary>
    private bool isTorchLit = false;

    /// <summary>
    /// The torchlit, torchUnlit, tutorialText and tutorialCollider properties are responsible for storing the torch lit, torch unlit, tutorial text and tutorial collider game objects respectively.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject torchLit, torchUnlit,tutorialText, tutorialCollider;

    /// <summary>
    /// The isTutorial property is responsible for storing if the torch is a tutorial torch or not.
    /// </summary>
    [SerializeField]
    private bool isTutorial;

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is detected and if the player interact input was triggred
    /// Íf these conditions are met, the LitTorch method is called to lit the torch.
    /// </summary>
    private void Update()
    {
        if (!isTorchLit && playerDetected && InteractInputTriggered())
        {
            LitTorch();

            if (isTutorial)
            {
                DestroyTutorialToLitTorch();
            }
        }
    }

    /// <summary>
    /// The DestroyTutorialToLitTorch method is responsible for destroying all the tutorial things related to the lit torch tutorial.
    /// This things are the tutorial text and the tutorial collider (invisible wall).
    /// </summary>
    private void DestroyTutorialToLitTorch()
    {
        Destroy(tutorialText);

        tutorialCollider.SetActive(false);

        isTutorial = false;
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method we are calling the behavior of the parent class of this method.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (isTutorial && playerDetected)
        {
            tutorialText.SetActive(true);
        }
    }

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method we are calling the behavior of the parent class of this method.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (isTutorial)
        {
            tutorialText.SetActive(false);
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
