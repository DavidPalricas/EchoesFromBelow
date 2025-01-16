using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private GameObject enemy;

    void Start()
    {
        healthBar.maxValue = enemy.GetComponent<Enemy>().maxHealth;
        healthBar.value = enemy.GetComponent<EntityFSM>().entitycurrentHealth;
    }

    void Update()
    {
        healthBar.value = enemy.GetComponent<EntityFSM>().entitycurrentHealth;
    }
}