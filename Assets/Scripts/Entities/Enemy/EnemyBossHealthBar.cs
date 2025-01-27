using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    
    private GameObject enemy;

    private void OnEnable()
    {  
       GameObject[] allEnemiens = GameObject.FindGameObjectsWithTag("Enemy");

        enemy = allEnemiens.Where(enemy => enemy.name.Contains("Boss")).FirstOrDefault();

        Debug.Log("Boss: " + enemy);

        if (enemy != null)
        {   
            int maxValue = enemy.GetComponent<Enemy>().maxHealth;

            healthBar.maxValue = maxValue;
            healthBar.value =  maxValue;
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