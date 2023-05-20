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

    void Awake()
    {
        stats = GetComponent<Stats>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }
    void FixedUpdate()
    {
        //Debug.Log(name);
        float characterSpeed = stats.Speed;
        rb.MovePosition(rb.position + movement * characterSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
        
    }
}
