using UnityEngine;

public class layerEstatua : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {   
        if (player != null)
        {
            if (player.transform.position.y < this.transform.position.y)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if (player.transform.position.y > this.transform.position.y)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }
    }
}
