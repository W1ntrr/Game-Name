using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D rb;

    float xSens;
    float ySens;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xSens = Input.GetAxis("Horizontal");
        ySens = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(xSens, ySens) * speed;
    }
}
