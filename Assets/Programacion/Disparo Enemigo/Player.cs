using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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

    //Referencias al script del enemigo y a su modelo de juego
    private Enemy enemyScriptReference;
    public GameObject enemyModel;


    //Booleana que detecta si el jugador está andando o no
    private bool walking = false;

    public Animator playerAnimator;

    public GameObject Test;

    //Referencia a la estela que deja el jugador
    public GameObject playerTrail;
    


    //Booleana para activar el parry
    public IEnumerator doAParry()
    {
        if (unableToParry == false)
        {
            //Activamos el parry
            unableToParry = true;
            parryActivated = true;


            playerAnimator.SetBool("finParry", false);
            Test.SetActive(true);


            playerAnimator.SetTrigger("heHechoParry");

            //Desactivamos el efecto del parry
            yield return new WaitForSeconds(0.2f);
            parryActivated = false;
            playerAnimator.SetBool("finParry", true);

            Test.SetActive(false);


            //Esperamos X tiempo antes de poder volver a activar el parry
            yield return new WaitForSeconds(0.25F);

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

        enemyScriptReference = enemyModel.GetComponent<Enemy>();

        
    }

    // Update is called once per frame
    void Update()
    {

        GroundCheck();
        move = Input.GetAxis("Horizontal");

        //Si estamos no estamos pulsando el botón de andado, entonces estamos quietos
        if(Input.GetAxis("Horizontal") == 0)
        {
            playerAnimator.SetBool("caminando", false);
        }
        else
        {
            playerAnimator.SetBool("caminando", true);
        }

        //Si no estamos tocando el suelo, estamos en el aire
        if(isGrounded == false)
        {
            playerAnimator.SetBool("flotando", true);

        }
        else
        {
            playerAnimator.SetBool("flotando", false);

        }


        

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));

            //Indicamos al animator que hemos saltado
            playerAnimator.SetTrigger("heSaltado");

            


        }

        //Si pulsas el botón derecho del ratón, se hace un parry
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(doAParry());
        }

        
        // Comprobamos la dirección de las pulsaciones
        float inputAxis = Input.GetAxisRaw("Horizontal");

        // Según la última tecla pulsada, activamos el flip o no
        if (inputAxis > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (inputAxis < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

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

                //Desactivamos la estela del jugador
                playerTrail.GetComponent<TrailRenderer>().enabled = false;

                //Devolvemos al jugador al origen del nivel y quitamos su velocidad
                transform.position = startPosition;
                rb.velocity = Vector2.zero;
                enemyScriptReference.timebetween = enemyScriptReference.starttimeb;

                //Desactivamos la estela del jugador
                playerTrail.GetComponent<TrailRenderer>().enabled = true;
                //Devolvemos la vida al jugador
                life = 2;
            }
        }
    }

   

}
