using UnityEngine;

public class CoffinTrigger : BaseInteractionTrigger
{
    [HideInInspector]
    public bool hasKey = false;

    [SerializeField]
    private GameObject keyPrefab;

    [SerializeField]
    private Level2Logic level2Logic;


    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && level2Logic.GraveYardEnemiesDead())
        {
            OpenCoffin();
        }
    }


    private void OpenCoffin()
    {   
        if (hasKey)
        {   
            var keySpawn = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);

            Instantiate(keyPrefab,keySpawn, Quaternion.identity);
        }
    }
}

