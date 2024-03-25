using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;

    [Header("Enemy Stats")]
    public float speed;
    public float damage;
    public float enemyLife;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyLife <= 0)
        {
            EnemyDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            enemyLife -= 1;
        }
    }

    public void EnemyDeath()
    {
        gameObject.SetActive(false);
    }
}
