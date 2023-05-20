using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject character;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stats collisionStats = collision.gameObject.GetComponent<Stats>();
        Destroy(gameObject);
        if (collisionStats != null)
        {
            collisionStats.CurrentHealth -= character.GetComponent<Stats>().Damage;
        }
       
    }


}
