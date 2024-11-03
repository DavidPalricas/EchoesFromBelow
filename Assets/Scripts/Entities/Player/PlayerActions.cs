using UnityEngine;

/// <summary>
/// The PlayerActions class is responsible for handling the player's actions.
/// </summary>
public class PlayerActions : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the value of the HasKey property.
    /// </summary>
    public bool HasKey { get; set; }

    /// <summary>
    /// The player property is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The gate property is responsible for storing the gate GameObject.
    /// </summary>
    private GameObject gate;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the player and gate variables and setting the HasKey property to false.
    /// </summary>
    private void Awake()
    {
        HasKey = false;
        player = GetComponent<Rigidbody2D>();
        gate = GameObject.Find("Gate");

    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is near the gate if the player is near the gate, the OpenGate method is called.
    /// </summary>
    private void Update()
    {
        if (IsGateNear())
        {   
            OpenGate();
        }
    }

    /// <summary>
    /// The isGateNear method is responsible for checking if the player is near the gate.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the gate is near otherwise, <c>false</c>.
    /// </returns>
    private bool IsGateNear()
    {
        float rayCastDistance = 1.5f;

        LayerMask gateLayer = LayerMask.GetMask("Default");

        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, rayCastDistance,gateLayer);

        return hit.collider != null || hit.collider.gameObject.name != "Gate";
    }

    /// <summary>
    /// The OpenGate method is responsible for opening the gate, if the player has the key and presses the E key.
    /// </summary>
    private void OpenGate()
    {
        if (Input.GetKeyDown(KeyCode.E) && HasKey){
            gate.SetActive(false);
        }
    }
}
