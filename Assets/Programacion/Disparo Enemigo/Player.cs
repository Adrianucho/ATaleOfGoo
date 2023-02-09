using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jump;
    private float life = 2;
    private float playerShoots;

    //Booleanas para activar y desactivar el parry y comprobar si ya está activo
    private bool parryActivated = false;
    private bool unableToParry = false;


    //Booleana para activar el parry
    public IEnumerator doAParry()
    {
        if (unableToParry == false)
        {
            Debug.Log("PARRY");
            //Activamos el parry
            unableToParry = true;
            parryActivated = true;

            //Esperamos X tiempo antes de desactivar el parry
            yield return new WaitForSeconds(2F);
            //Desactivamos el parry
            parryActivated = false;
            unableToParry = false;
        }
        

    }


    private float move;

    public Rigidbody2D rb;

    const float groundCheckRadius = 0.2F;

    [SerializeField] Transform groundCheckCollider;
    public bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GroundCheck();
        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));

        }

        //Si pulsas el botón derecho del ratón, se hace un parry
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(doAParry());
        }



    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;

        }
    }

    //Método que hacer perder vida al jugador
    void checkDamageStatus()
    {
        life = life - 1;

        if(parryActivated == true)
        {
            if(playerShoots >= 3)
            {
                life = life - 1;
            }
            else
            {
                playerShoots = playerShoots + 1;
            }
        }
        else
        {
            life = life - 1;
        }


    }

}
