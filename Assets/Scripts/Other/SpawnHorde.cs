using UnityEngine;

/// <summary>
///    The SpawnHorde class is responsible for spawning a horde of enemies.
/// </summary>
public class SpawnHorde : MonoBehaviour
{
    /// <summary>
    /// The hordeSize property represents the number of enemies that will be spawned.
    /// It is serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private int hordeSize;

    /// <summary>
    /// The enemyPrefab property stores the prefab of the enemy that will be spawned.
    /// It is serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject enemyPrefab;

    /// <summary>
    /// The maxSpawnTime property represents the maximum time between enemy spawns.
    /// It is serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private float maxSpawnTime;

    /// <summary>
    /// The minSpawnTime property represents the minimum time between enemy spawns.
    /// It is serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private float minSpawnTime;

    /// <summary>
    /// The enemiesSpawned property represents the number of enemies that have been spawned.
    /// </summary>
    private int enemiesSpawned = 0;

    /// <summary>
    /// The spawnTime property represents the time between enemy spawns.
    /// </summary>
    private float spawnTime = 0f;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the spawnTime property is initialized, by calling the GetSpawnTime method.
    /// </summary>
    private void Awake()
    {
        spawnTime = GetSpawnTime();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// This method will decrement the spawnTime property by the time that has passed since the last frame, while the number of enemies spawned is less than the hordeSize.
    /// Whene the spawnTime property is less than or equal to 0, the SpawEnemies method is called to spawn the enemy and the spawnTime property is reset by calling the GetSpawnTime method.
    /// </summary>
    private void Update()
    {
        if (enemiesSpawned < hordeSize)
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0)
            {
                SpawEnemies();
                spawnTime = GetSpawnTime();
            }
        }
    }

    /// <summary>
    /// The GetSpawnTime method is responsible for returning a random float which represents the time of an enemy to spawn.
    /// </summary>
    /// <returns>A random float which represents the time of an enemy to spawn</returns>
    private float GetSpawnTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    /// <summary>
    /// The SpawEnemies method is responsible for spawning an enemy, and incrementing the enemiesSpawned property.
    /// </summary>
    private void SpawEnemies()
    {   
        Vector2 enemyPosition = GetEnemySpawnPosition();
        GameObject enemy =  Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

        Debug.Log("Enemy spawned at: " + enemyPosition);

        enemy.GetComponent<EnemyMovement>().IsIdle = false;

        enemiesSpawned++;
    }

    /// <summary>
    /// The GetEnemySpawnPosition method is responsible for getting a random position for the enemy to spawn, inside the spawn area.
    /// It generates a random angle and a random distance from the center of the spawn area to calculate a position within a circular area.
    /// The position is guaranteed to be inside the area because the distance is always between 0 and the radius of the circle, ensuring
    /// that the enemy will spawn within the defined radius from the spawn center.
    /// </summary>
    /// <returns>A random position (Vector2) inside the spawn area where the enemy can spawn.</returns>
    private Vector2 GetEnemySpawnPosition()
    {
        Vector2 spawnAreaCenter = transform.position;

        float radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;

        Vector2 enemyPosition;
        float angle, distance;

        do
        {  
            angle = Random.Range(0f, 2f * Mathf.PI);
            distance = Random.Range(0f, 2f) * radius;

            enemyPosition = new Vector2(spawnAreaCenter.x + distance * Mathf.Cos(angle), spawnAreaCenter.y + distance * Mathf.Sin(angle));

        } while (!InSpawnArea(enemyPosition, radius, spawnAreaCenter));

        return enemyPosition;
    }

    /// <summary>
    /// The InSpawnArea method is responsible for checking if the enemy spawn positions is in the spawn area.
    /// To do this, it uses the equation of a circle to check if the enemy position is inside the circle.
    /// </summary>
    /// <param name="enemyPosition">The enemy position.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="spawnAreaCenter">The area center.</param>
    /// <returns></returns>
    private bool InSpawnArea(Vector2 enemyPosition, float radius, Vector2 spawnAreaCenter)
    {
        return Mathf.Pow(enemyPosition.x - spawnAreaCenter.x, 2) + Mathf.Pow(enemyPosition.y - spawnAreaCenter.y, 2) <= Mathf.Pow(radius, 2);
    }
}
