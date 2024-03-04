using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Añadir la librería que permite usar enl new input system

public class PlayerControllerMulti : MonoBehaviour
{
    //Variables de referencia privadas
    Animator anim; //Para cambiar entre animaciones
    Rigidbody2D playerRb; //Para aplicar fuerzas físicas (movimiento, salto, ..)
    PlayerInput playerInput; //Para leer las nuevas inputs
    Vector2 horInput; //Para almacenar el input izqda/dech de todos los disposistivos
    public enum PlayerState { normal, damaged}

    [Header("Character Stats & Status")]
    public float speed;
    public float jumpForce;
    [SerializeField] bool isFacingRight;
    [SerializeField] bool canAttack;
    [SerializeField] PlayerState currentState;

    [Header("GroundCheck Configuration")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        currentState = PlayerState.normal;
    }

    

    // Update is called once per frame
    void Update()
    {
        //Detección de la capa ground para no hacer saltos infinitos
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Lectura del input de movimiento constante SOLO sie el player esta en el estado deseado (estado normal)
        if (currentState == PlayerState.normal)
        {
            horInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.normal) { Movement(); }
    }

    void Movement()
    {
        playerRb.velocity = new Vector2(horInput.x * speed, playerRb.velocity.y);
    }
}
