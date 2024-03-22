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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
