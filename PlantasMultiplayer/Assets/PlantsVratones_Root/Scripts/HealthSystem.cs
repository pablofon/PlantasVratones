using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Stats")]
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            GameManager.Instance.gameOver = true;
        }
        else
        {
            GameManager.Instance.gameOver = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
