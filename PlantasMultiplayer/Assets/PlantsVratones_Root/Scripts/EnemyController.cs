using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;

    [Header("Enemy Stats")]
    public float speed;
    public int damage;
    public float enemyLife;
    public Transform basePosition;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = GameObject.FindGameObjectWithTag("Water").transform;

        Vector3 currentScale = transform.localScale;
        currentScale.y *= -1;
        transform.localScale = currentScale;
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

        if (collision.gameObject.CompareTag("Water"))
        {
            HealthSystem hpSystem = collision.gameObject.GetComponent<HealthSystem>();
            hpSystem.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public void EnemyDeath()
    {
        gameObject.SetActive(false);
    }

    public void Run()
    {
        transform.position = Vector2.MoveTowards(transform.position, basePosition.position, speed * Time.deltaTime);

        Vector2 direction = basePosition.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.back * angle);
    }
}
