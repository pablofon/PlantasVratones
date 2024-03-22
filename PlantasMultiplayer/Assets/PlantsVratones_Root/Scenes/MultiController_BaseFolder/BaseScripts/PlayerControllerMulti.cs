using System;
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
    public enum PlayerState { normal, sprinting, damaged}
    private bool isSprinting, isDamaged;

    [Header("Character Stats & Status")]
    public float speed, normalSpeed, sprintSpeed, damagedSpeed;
    public float jumpForce;
    public float restablishCooldown = 2f;
    [SerializeField] bool isFacingRight;
    [SerializeField] bool canAttack;
    [SerializeField] PlayerState currentState;

    [Header("GroundCheck Configuration")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    [Header("Knockback Configurationj")]
    public float knockbackX; //Fuerza de empuje en X
    public float knockbackY; //Fuerza de empuje en Y
    public float knockbackMultiplier = 1; //multiplicador de empuje, si quiero que haya empuje, ha de ser mínimo 1
    Vector2 knockbackForce; //Fuerca total de empuje
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        currentState = PlayerState.normal;
        canAttack = true;
        isFacingRight = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        //Detección de la capa ground para no hacer saltos infinitos
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Detector contínuo de si tenemos que flipear
        FlipUpdater();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && currentState == PlayerState.normal)
        {
            //Trigerear animaciones aquí
            currentState = PlayerState.damaged;
            isDamaged = true;
            

            //KNOCKBACK SEGÚN POSICIÓN DEL QUE GOLPEA
            //Si el que pega la patada esta a la izquierda...
            if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                //Knockback haca el x positivo
                knockbackForce = new Vector2(knockbackX, knockbackY); //Determinar la fuerza de empuje hacia la derecha
                playerRb.AddForce(knockbackForce * knockbackMultiplier); //Aplica fuerza por multiplicador (si no lo hay debe ser 1)

            }
            else //Si el que pega la patada esta a la derecha
            {
                //Knockback haca el x negativo
                knockbackForce = new Vector2(-knockbackX, knockbackY); //Determinar la fuerza de empuje hacia la izquierda
                playerRb.AddForce(knockbackForce * knockbackMultiplier); //Aplica fuerza por multiplicador (si no lo hay debe ser 1)
            }
            Invoke(nameof(ResetStatus), restablishCooldown);
        }
    }

    void ResetStatus()
    {
        currentState = PlayerState.normal;
        isDamaged = false;
    }

    void Movement()
    {
        //Calculo de la velocidad según el estado del personaje
        //Uso del operador ternario (?)
        speed = isDamaged ? damagedSpeed : (isSprinting ? sprintSpeed : normalSpeed);
        // Ejemplo: speed = isSprinting ? sprintSpeed : normalSpeed;
        //Ejecución del movimiento en si
        playerRb.velocity = new Vector2(horInput.x * speed, playerRb.velocity.y);
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

    public void Jump (InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            if (currentState == PlayerState.normal)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && currentState == PlayerState.normal)
        {
            if (canAttack)
            {
                anim.SetTrigger("Attack");
                canAttack = false;
                Invoke(nameof(ResetAttack), 2f);
            }
            
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
        //ReadValueAsButton() "imita" el mantener pulsado un boton del antiguo input system
        //se suele asociar a bools. Es decir, cuendo mantenemos el botón se activa (true) un estado.
        //En otra parte del código pondremos un condicional que define que en estado X pasa cosa X
    }
}
