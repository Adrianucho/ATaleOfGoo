using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;
    public float jump;
    private float life = 2;
    public float playerShoots;

    //Booleanas para activar y desactivar el parry y comprobar si ya está activo
    private bool parryActivated = false;
    private bool unableToParry = false;

    //Posición inicial del jugador
    private Vector2 startPosition;

 

    //Booleana para activar el parry
    public IEnumerator doAParry()
    {
        if (unableToParry == false)
        {
            //Activamos el parry
            unableToParry = true;
            parryActivated = true;

            //Desactivamos el efecto del parry
            yield return new WaitForSeconds(0.5f);
            parryActivated = false;

            //Esperamos X tiempo antes de poder volver a activar el parry
            yield return new WaitForSeconds(2F);

            //Volvemos a ser capaces de usar el parry
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

        //Capturamos la posición inicial del jugador para así poder llamarlo de vuelta cuando muera
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerShoots);
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
    public void checkDamageStatus()
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

            if(life > 0)
            {
                life = life - 1;
            }
            else
            {

                //Devolvemos al jugador al origen del nivel y quitamos su velocidad
                transform.position = startPosition;
                rb.velocity = Vector2.zero;

                //Devolvemos la vida al jugador
                life = 2;
            }
            
        }


    }

}
