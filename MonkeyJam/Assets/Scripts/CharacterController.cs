using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 movement;
    public Camera cam;
    Vector2 mousePos;
    private Stats stats;
    private bool isDashing = false;
    private float dashDuration = 0.5f;
    private float dashSpeedMultiplier = 2f;
    private float currentDashTime = 0f;
    public float dashCooldown = 3;
    bool dashOnCooldown = false;
    public Animator animator;

    void Awake()
    {
        stats = GetComponent<Stats>();
    }

    void Update()
    {
       
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x > 0 || movement.y > 0 || movement.x < 0 || movement.y < 0)
        {
        
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);

        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            if (dashOnCooldown == false)
            {
                StartDash();
                StartCoroutine(DashCooldown());
                StartCoroutine(DashTimerAnimation());
            }
            
        }
    }

    void FixedUpdate()
    {
        float characterSpeed = stats.Speed;

        if (isDashing)
        {
            currentDashTime += Time.fixedDeltaTime;

            if (currentDashTime >= dashDuration)
            {
                StopDash();
            }
            else
            {
                characterSpeed *= dashSpeedMultiplier;
            }
        }
       
        rb.MovePosition(rb.position + movement * characterSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    private void StartDash()
    {
        isDashing = true;
        currentDashTime = 0f;
    }

    private void StopDash()
    {
        isDashing = false;
    }

    IEnumerator DashCooldown()
    {
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }

    public IEnumerator DashTimerAnimation()
    {

        animator.SetBool("Rolling", true);
        yield return new WaitForSeconds(dashDuration);
        animator.SetBool("Rolling", false);
    }
}