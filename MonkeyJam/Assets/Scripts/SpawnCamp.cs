using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCamp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public List<GameObject> pooledEnemies = new List<GameObject>();
    public float timeBetweenSpawns;
    public int pooledQuantity;


    public void SetTimeBetweenSpawns(float time)
    {
        if (time < 0)
        {
            timeBetweenSpawns = 0;
        }
        else
        {
            timeBetweenSpawns = time;
        }
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public void PollEnemies()
    {
       
            for (int j = 0; j < pooledQuantity; j++)
            {
                GameObject enemy = Instantiate(enemyPrefab, gameObject.transform);
                enemy.SetActive(false);
                pooledEnemies.Add(enemy);
            }
        
    }

    private List<GameObject> GetUnactiveEnemies()
    {
        return pooledEnemies.FindAll(e => !e.activeInHierarchy);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
