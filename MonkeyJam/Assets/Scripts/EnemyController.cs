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
    public bool isMeleeAttack;
    public bool isRangedAttack;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public int maxBullets = 10;
    public int bulletCounter;
    public float bulletForce = 20f;
    public float reloadSpeed;
    private bool reloading;
    private float shootDelay = 0.5f; // Adjust this value to set the desired delay between shots
    private float shootTimer = 0f;
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
            if (isMeleeAttack)
            {
                meleeAttack();
            }
            else if (isRangedAttack)
            {
                rangedAttack();
            }
           
        }
    }

    void meleeAttack()
    {
        player.GetComponent<Stats>().CurrentHealth -= stats.Damage;
        Debug.Log(player.GetComponent<Stats>().CurrentHealth);
        attackCooldown = true;
    }

    void rangedAttack()
    {
        //if (bulletCounter > 0 && reloading == false)
        //{
            //bulletCounter -= 1;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 playerDirection = (player.transform.position - firePoint.position).normalized;
            rb.AddForce(playerDirection * bulletForce, ForceMode2D.Impulse);
        //}
        //else if (bulletCounter == 0 && reloading == false)
        //{
        //    StartCoroutine(Reload());
        //}
    }
    //IEnumerator Reload()
    //{
    //    //animator.SetBool("Reloading", true);
    //    reloading = true;
    //    yield return new WaitForSeconds(reloadSpeed);
    //    bulletCounter = maxBullets;
    //    reloading = false;
    //    //animator.SetBool("Reloading", false);
    //    //isFirstShot = true;


    //}

    void Update()
    {
        if (player)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x);

            if (isMeleeAttack)
            {
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
            else if (isRangedAttack)
            {
               
                if (distance < viewRange)
                {
                    shootTimer += Time.deltaTime;
                    if (shootTimer >= shootDelay)
                    {
                        Attack();
                        shootTimer = 0f;
                        Quaternion newRotation = Quaternion.Euler(Vector3.forward * (angle * Mathf.Rad2Deg - 90f));
                        transform.rotation = newRotation;

                    }
                    if (distance > attackRange)
                    {
                        transform.SetPositionAndRotation(Vector2.MoveTowards(this.transform.position, player.transform.position, stats.Speed
                            * Time.deltaTime), Quaternion.Euler(Vector3.forward * (angle * Mathf.Rad2Deg - 90f)));
                            
                            
                    }
                } 
            }
        }
       
    }
}
