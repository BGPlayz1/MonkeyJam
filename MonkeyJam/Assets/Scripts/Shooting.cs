using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int maxBullets = 10;
    public int bulletCounter;
    public float bulletForce = 20f;
    public float reloadSpeed;
    private bool reloading;
    public Animator animator;
    private bool isShooting = false;
    private bool isFirstShot = true;
    private float shootDelay = 0.5f; // Adjust this value to set the desired delay between shots
    private bool isFirstShotDelayRunning = false;
    private float shootTimer = 0f;

    private void Awake()
    {
        bulletCounter = maxBullets;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Shooting", true);
            isShooting = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Shooting", false);
            isShooting = false;
            if (!isFirstShotDelayRunning)
            {
                StartCoroutine(SetFirstShotDelay());
            }
        }

        if (isShooting)
        {
            shootTimer += Time.deltaTime;
            if (isFirstShot || shootTimer >= shootDelay)
            {
                Shoot();
                shootTimer = 0f;
                isFirstShot = false;
            }
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());


        }
    }


    IEnumerator SetFirstShotDelay()
    {
        isFirstShotDelayRunning = true;
        yield return new WaitForSeconds(1f);
        isFirstShot = true;
        isFirstShotDelayRunning = false;
    }
    IEnumerator Reload()
    {
        animator.SetBool("Reloading", true);
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        bulletCounter = maxBullets;
        reloading = false;
        animator.SetBool("Reloading", false);
        isFirstShot = true;


    }
    void Shoot()
    {
        if (bulletCounter > 0 && reloading == false)
        {
            bulletCounter -= 1;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (bulletCounter == 0 && reloading == false)
        {
            StartCoroutine(Reload());
        }
    }
}
