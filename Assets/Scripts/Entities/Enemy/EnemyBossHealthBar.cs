using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    
    private GameObject enemy;

    void Start()
    {
        enemy = GameObject.Find("Skeleton Boss(Clone)");

        if (enemy != null)
        {
            healthBar.maxValue = enemy.GetComponent<Enemy>().maxHealth;
            healthBar.value = enemy.GetComponent<EntityFSM>().entitycurrentHealth;
        }
    }

    void Update()
    {
        if (enemy != null)
        {
            healthBar.value = enemy.GetComponent<EntityFSM>().entitycurrentHealth;
        }

        if (GameObject.Find("Level1").GetComponent<Rank>().BossKilled)
        {
            gameObject.SetActive(false);
        }
    }
}