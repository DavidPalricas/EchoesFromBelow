using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    
    private GameObject enemy;


    private int maxValue;

    private void OnEnable()
    {
        enemy = GameObject.Find("Skeleton Boss(Clone)");

        if (enemy != null)
        {   
            maxValue = enemy.GetComponent<Enemy>().maxHealth;
            healthBar.maxValue = maxValue;
            healthBar.value = enemy.GetComponent<EntityFSM>().entitycurrentHealth;
        }
    }

    void Update()
    {
        if (enemy != null)
        {
            healthBar.value = enemy.GetComponent<EntityFSM>().entitycurrentHealth;

            if (healthBar.value <= 0)
            {
                gameObject.SetActive(false);
                enabled = false;
            }
        }
    }
}