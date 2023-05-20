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
        // Subscribe to death event
        Spawner.ForEach(Spawn =>
        {
            GameObject NewEnemy = Instantiate(Spawn.Type,
            new Vector3(Random.Range(AreaMin.x, AreaMax.x), Random.Range(AreaMin.y, AreaMax.y), 0),
            Quaternion.identity);

            // duplicated code too lazy to do a function
            EnemyRespawnable NewEnemyRespawnable = NewEnemy.GetComponent<EnemyRespawnable>();
            NewEnemyRespawnable.RespawnTime = Spawn.RespawnTime;
            NewEnemyRespawnable.OnDeathMessage += TriggerDeathSpawnTimer;
            // End of duplcated code
        });
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
