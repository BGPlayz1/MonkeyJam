using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletCounter;
    public float bulletForce = 20f;
    private bool reloading;
    public Animator animator;
    private bool isShooting = false;
    private bool isFirstShot = true;
    private bool isFirstShotDelayRunning = false;
    private float shootTimer = 0f;
    public TextMeshProUGUI reloadText;

    [SerializeField]
    private float shootDelay; // Adjust this value to set the desired delay between shots
    public float ShootDelay
    {
        get { return shootDelay; }  
        set
        {
            shootDelay = value;
        }
    }
    private float maxBullets; 
    public float MaxBullets
    {
        get { return maxBullets; }
        set
        {
            maxBullets = value;
        }
    }
    private float reloadSpeed;
    public float ReloadSpeed
    {
        get { return reloadSpeed; }
        set
        {
            reloadSpeed = value;
        }
    }
    private void Awake()
    {
        reloadSpeed = 3f;
        shootDelay = 0.5f;
        maxBullets = 10;
        bulletCounter = maxBullets;

        UpdateBulletCounterText();
    }

    private void UpdateBulletCounterText()
    {
        reloadText.text = bulletCounter.ToString() + "/" + maxBullets.ToString(); // Update the text value with the bullet counter
    }

    

    void Update()
    {
        UpdateBulletCounterText();
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
