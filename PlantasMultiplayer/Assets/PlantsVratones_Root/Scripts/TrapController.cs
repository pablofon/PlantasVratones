using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    Animator anim;

    [Header("Trap Stats")]
    public float speed;
    public Transform basePosition;
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Run();

        if (!isAlive)
        {
            TrapDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Bomb();
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            isAlive = false;
        }
    }

    public void Run()
    {
        transform.position = Vector2.MoveTowards(transform.position, basePosition.position, speed * Time.deltaTime);
    }

    public void Bomb()
    {

    }

    public void TrapDeath()
    {
        gameObject.SetActive(false);
    }
}
