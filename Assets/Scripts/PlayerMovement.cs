using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 movement;

    public TrailRenderer dashTrail;
    public Camera FOV;

    [SerializeField] private float speed;
    private float originalSize = 6.5f;

    [Header("Cooldown")]
    [SerializeField] bool coolingDown = false;
    [SerializeField] private float dashCooldown = 1f;
    public Image DashSlider;

    [Header("Dash")]
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashDuration = 0.5f;
    private bool canDash = true;
    private bool isDashing;

    void Start()
    {
        FOV = GameObject.FindAnyObjectByType<Camera>();
        dashTrail = gameObject.GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        dashTrail.emitting = false;
        DashSlider.fillAmount = 1;
        FOV.orthographicSize = originalSize;
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

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && !coolingDown)
        {
            StartCoroutine(Dash());
            Activate();
            StartCoroutine(ZoomInOut(FOV.orthographicSize, 8f, dashDuration));
        }

        if(coolingDown)
        {
            CoolDown();
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

    private IEnumerator ZoomInOut(float currentSize, float MaxSize, float time)
    {
        float elapsed = 0;

        while (elapsed <= time)
        {
            elapsed += Time.deltaTime * dashDuration;
            float t = Mathf.Clamp01(elapsed / time);

            float smoothT = Mathf.SmoothStep(0, 1, t);

            FOV.orthographicSize = Mathf.Lerp(originalSize, MaxSize, smoothT);

            if (!isDashing)
            {
                FOV.orthographicSize = Mathf.Lerp(MaxSize, originalSize, smoothT);
            }
            yield return null;
        }
    }

    void CoolDown()
    {
        DashSlider.fillAmount += dashDuration * Time.deltaTime;
        if (DashSlider.fillAmount == 1)
        {
            coolingDown = false;
        }
    }

    void Activate()
    {
        DashSlider.fillAmount = 0;
        coolingDown = true;
    }
}
