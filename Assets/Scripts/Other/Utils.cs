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
}
