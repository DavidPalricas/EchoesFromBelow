using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The Utils class is responsible for storing utility methods that can be used in different parts of the game.
/// Such as normalizing a direction vector.
/// </summary>
public static class Utils
{
    /// <summary>
    /// The  GetUnitaryVector method is responsible for getting a unitary vector from a direction vector.
    /// </summary>
    /// <returns>A unitary vector </returns>
    public static Vector2 GetUnitaryVector(Vector2 directionVector)
    {
        float xDirection = Mathf.Round(directionVector.x);
        float yDirection = Mathf.Round(directionVector.y);

        return new Vector2(xDirection, yDirection);
    }

    /// <summary>
    /// The CollectableItemType enum is responsible for storing the types of the collectable items of the game.
    /// </summary>
    public enum CollectableType
    {
        HealItem,
        Key,
        Sword,
        Stick,
        SlingShot
    }

    /// <summary>
    /// The WaitForAnimationEnd method is responsible for waiting for a specific animation to end.
    /// After the animation ends, a function or method is called.
    /// </summary>
    /// <param name="animator">The animator of the game object.</param>
    /// <param name="animationName">Name of the animation.</param>
    /// <param name="callback">The method or function called after the animation ends.</param>
    /// <returns>An IEnumerator used to control the coroutine. Returns null if the animation is not found.</returns>
    public static IEnumerator WaitForAnimationEnd(Animator animator, string animationName, System.Action callback)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(animationState.length);

        callback?.Invoke();
    }

    /// <summary>
    /// The Wait method is responsible for waiting for a specific time.
    /// </summary>
    /// <param name="time">The time to wait (seconds).</param>
    /// <param name="action">Action to execute after waiting.</param>
    /// <returns></returns>
    public static IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    /// <summary>
    /// The IsPlayerAlive method is responsible for checking if the player is alive.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the player is alive otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPlayerAlive()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        return player.GetComponent<Player>().entityFSM.entitycurrentHealth > 0;
    }


    /// <summary>
    /// The LoadAndApplyBindings method is responsible for loading and applying if there are any player's input preferences saved.
    /// </summary>
    public static void LoadAndApplyBindings(PlayerInput playerInput)
    {
        string rebinds = PlayerPrefs.GetString("inputBindings", string.Empty);

        if (!string.IsNullOrEmpty(rebinds))
        {
            playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        }
    }

    /// <summary>
    /// The isSpeechActive property is responsible for storing if the speech box is active.
    /// </summary>
    public static bool isSpeechActive = false;
}
