using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject enemy;

    private void Awake()
    {
        Destroy(gameObject, 2f);

    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            Stats collisionStats = collision.gameObject.GetComponent<Stats>();
            Destroy(gameObject);
            if (collisionStats != null)
            {
                collisionStats.CurrentHealth -= enemy.GetComponent<Stats>().Damage;
                Debug.Log(collisionStats.CurrentHealth);
            }
        }
    }
    


}
