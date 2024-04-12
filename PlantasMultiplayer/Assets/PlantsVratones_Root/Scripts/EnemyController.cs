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
    public Transform basePosition;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = GameObject.FindGameObjectWithTag("Water").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyLife <= 0)
        {
            EnemyDeath();
        }

        Run();
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

    public void Run()
    {
        transform.position = Vector2.MoveTowards(transform.position, basePosition.position, speed * Time.deltaTime);
    }
}
