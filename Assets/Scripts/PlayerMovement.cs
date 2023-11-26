using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 movement;

    [SerializeField] private TrailRenderer dashTrail;

    [SerializeField] private float speed;

    [Header("Cooldown")]
    public Slider DashSlider;

    [Header("Dash")]
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;

    void Start()
    {
        dashTrail = gameObject.GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        dashTrail.emitting = false;
    }

    void Update()
    {


        if (isDashing)
        {
            return;
        }

        movement.Normalize();

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(movement.x, movement.y) * speed;

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false; // We can't dash
        dashTrail.emitting = true;
        isDashing = true; // because we currently dashing   
        rb.velocity = new Vector2(movement.x, movement.y) * dashPower; // Set our velocity to increase dash power
        yield return new WaitForSeconds(dashDuration); // for a set amount of time and wait for it to elapse. **Basically how long the dash is**
        isDashing = false; // Afterwards, we are no longer dashing
        dashTrail.emitting = false;

        yield return new WaitForSeconds(dashCooldown); // Wait for our cooldown until
        canDash = true; // we are able to dash again
    }
}
