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

    private void Awake()
    {
        bulletCounter = maxBullets;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

            Debug.Log("reloading");

        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        bulletCounter = maxBullets;
        reloading = false;

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
