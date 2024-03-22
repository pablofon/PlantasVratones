using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Variables de referencia
    Animator anim;
    Rigidbody2D playerRb;
    PlayerInput playerInput;
    Vector2 horInput;
    Vector2 vertInput;
    public enum PlayerState { normal, damaged}
    private bool isDamaged;

    [Header("Plants Stats & Status")]
    public float speed;
    public float restablishCoolDown = 2f;
    [SerializeField] bool isFacingRight;
    [SerializeField] PlayerState currentState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        currentState = PlayerState.normal;
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        FlipUpdater();

        //Lectura del input de movimiento constante SOLO sie el player esta en el estado deseado (estado normal)
        if (currentState == PlayerState.normal)
        {
            horInput = playerInput.actions["Movement"].ReadValue<Vector2>();
            vertInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        }
    }
    private void FixedUpdate()
    {
        if (currentState == PlayerState.normal) { Movement(); }
    }

    void Movement()
    {
        playerRb.velocity = new Vector2(horInput.x * speed, vertInput.y * speed);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    void FlipUpdater()
    {
        if (horInput.x > 0)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if (horInput.x < 0)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }
}
