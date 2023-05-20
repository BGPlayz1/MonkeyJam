using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public Transform attackPoint;
    private float distance;
    public float attackRange;
    public float viewRange;
    bool attackCooldown;
    public float attackCooldownLength;
    public Stats stats;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            attackPoint = player.transform;
    }

    // Update is called once per frame


    public IEnumerator AttackCooldown()
    {
        if (attackCooldown)
        yield return new WaitForSeconds(attackCooldownLength);
        attackCooldown = false;
    }
    void Attack()
    {
        if (player.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Stats>().CurrentHealth -= stats.Damage;
            Debug.Log(player.GetComponent<Stats>().CurrentHealth);
            attackCooldown = true;
        }
    }

    void Update()
    {
        if (player)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x);
            if (distance > attackRange)
            {
                if (distance < viewRange)
                {
                    //rb.MovePosition(Vector2.MoveTowards(this.transform.position, player.transform.position, stats.Speed
                    //    * Time.fixedDeltaTime));
                    //rb.rotation = angle * Mathf.Rad2Deg - 90f;  // this code makes enemy have knockback
                    transform.SetPositionAndRotation(Vector2.MoveTowards(this.transform.position, player.transform.position, stats.Speed
                        * Time.deltaTime), Quaternion.Euler(Vector3.forward * (angle * Mathf.Rad2Deg - 90f)));
                }
            }
            else
            {
                if (attackCooldown == false)
                {
                    Attack();
                    StartCoroutine(AttackCooldown());
                }

            }
        }
       
    }
}
