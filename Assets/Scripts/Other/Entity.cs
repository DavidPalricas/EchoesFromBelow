using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] int hp;

    public  float Speed { get; private set; }

    public  Rigidbody2D Body { get; set; }


    public static readonly Vector2[] Directions = new Vector2[]
    {
        Vector2.left,
        Vector2.right,
        Vector2.down,
        Vector2.up,
        new (-1, -1), //Left Down Diagonal
        new (-1, 1), // Left Up Diagonal
        new (1, -1), // Right Down Diagonal
        new (1, 1), // Right Up Diagonal
    };

    private void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Speed = speed;
    }
}
