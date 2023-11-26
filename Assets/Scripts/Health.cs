using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float delay = 1f;
    private float timer;
    

    public int health;
    public int numOfHearrts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject hurtEffect;
    public GameObject Blood;
    private void Update()
    {
        timer += Time.deltaTime;

        if(health > numOfHearrts)
        {
            health = numOfHearrts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearrts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(timer > delay)
            {
                health -= 1;
                Instantiate(hurtEffect, transform.position, Quaternion.identity);
            }
        }
        if(health == 0)
        {
            Destroy(gameObject, 0.3f);
            Instantiate(Blood, transform.position, Quaternion.identity);
        }
    }
}
