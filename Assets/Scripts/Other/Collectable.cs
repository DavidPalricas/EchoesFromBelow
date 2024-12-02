using UnityEngine;

/// <summary>
/// The CollectableType class is responsible for storing the type of the collectable item.
/// </summary>
public class Collectable : MonoBehaviour
{
    /// <summary>
    /// The collectableType property is responsible for storing the type of the collectable item.
    /// Its set to public so it can be accessed in other classes.
    /// </summary>
    public Utils.CollectableType type;

    /// <summary>
    /// The isCollected property is responsible for storing whether the collectable item was collected or not.
    /// </summary>
    [HideInInspector]
    public bool isCollected = false;
}
