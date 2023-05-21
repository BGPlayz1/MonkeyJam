using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAreaSpawner : MonoBehaviour
{
    [SerializeField]
    private List<SpawnerScriptable> Spawner;
    [SerializeField]
    private Vector2 AreaMin;
    [SerializeField]
    private Vector2 AreaMax;

    public void Awake()
    {
        Vector3 referencePosition = transform.position;
        // Subscribe to death event
        Spawner.ForEach(Spawn =>
        {
            GameObject newEnemy = Instantiate(Spawn.Type,
                GetRandomSpawnPosition(transform.position, AreaMin, AreaMax),
                Quaternion.identity);

            // duplicated code too lazy to do a function
            EnemyRespawnable newEnemyRespawnable = newEnemy.GetComponent<EnemyRespawnable>();
            newEnemyRespawnable.RespawnTime = Spawn.RespawnTime;
            newEnemyRespawnable.OnDeathMessage += TriggerDeathSpawnTimer;
            // End of duplicated code
        });
    }

    private Vector3 GetRandomSpawnPosition(Vector3 referencePosition, Vector2 areaMin, Vector2 areaMax)
    {
        float randomX = Random.Range(referencePosition.x + areaMin.x, referencePosition.x + areaMax.x);
        float randomY = Random.Range(referencePosition.y + areaMin.y, referencePosition.y + areaMax.y);
        return new Vector3(randomX, randomY, 0f);
    }
    IEnumerator Respawn(GameObject Type, float RespawnTime)
    {
        // Need to unsubscribe the event else we get memory leak
        EnemyRespawnable EnemyRespawnable = Type.GetComponent<EnemyRespawnable>();
        EnemyRespawnable.OnDeathMessage -= TriggerDeathSpawnTimer;

        yield return new WaitForSeconds(RespawnTime);
        EnemyRespawnable.OnDeathMessage += TriggerDeathSpawnTimer;
        Type.transform.position = new Vector3(Random.Range(AreaMin.x, AreaMax.x), Random.Range(AreaMin.y, AreaMax.y), 0);
        Type.SetActive(true);
    }

    public void TriggerDeathSpawnTimer(GameObject Type, float RespawnTime)
    {
        StartCoroutine(Respawn(Type, RespawnTime));
    }
}
