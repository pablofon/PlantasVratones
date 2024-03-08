using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //A�adir la librer�a que permite usar enl new input system

public class PlayerControllerMulti : MonoBehaviour
{
    //Variables de referencia privadas
    Animator anim; //Para cambiar entre animaciones
    Rigidbody2D playerRb; //Para aplicar fuerzas f�sicas (movimiento, salto, ..)
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
        canAttack = true;
        isFacingRight = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        //Detecci�n de la capa ground para no hacer saltos infinitos
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Detector cont�nuo de si tenemos que flipear
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

    void Movement()
    {
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
        //Lo haremos en la proxima clase
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
}
