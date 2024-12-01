using System.Collections;
using UnityEngine;
/// <summary>
/// The Utils class is responsible for storing utility methods that can be used in different parts of the game.
/// Such as normalizing a direction vector.
/// </summary>
public static class Utils 
{
    /// <summary>
    /// The NormalizeDirectionVector method is responsible for normalizing a direction vector.
    /// </summary>
    /// <returns>A normal vector </returns>
    public static Vector2 NormalizeDirectionVector(Vector2 directionVector)
    {
        // Normalize the  direction vector, examples of normalized vectors: (-1, 0), (0, 1), (1, 0), (0, -1), (-1, -1), (-1, 1), (1, -1), (1, 1)
        Vector2 vectorToBeNormalized = (directionVector).normalized;

        // Some cases the line above can produce results like (0, -0.4), or (0.8,-0.6).
        // To avoid these issues, we round the values to get the closest integer value.
        return new Vector2(Mathf.Round(vectorToBeNormalized.x), Mathf.Round(vectorToBeNormalized.y));
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
}
