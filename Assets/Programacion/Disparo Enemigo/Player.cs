using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{

    public float speed;
    public float jump;
    public float life = 1;
    public float playerShoots;

    //Booleanas para activar y desactivar el parry y comprobar si ya está activo
    private bool parryActivated = false;
    private bool unableToParry = false;

    //Posición inicial del jugador
    private Vector2 startPosition;

    //Referencias al script del enemigo y a su modelo de juego
    private Enemy enemyScriptReference;
    public GameObject enemyModel;

 


    //Declaro la 
    AudioSource audioSalto, audioMuerte, audioParry, audioDano;

    //Booleana que detecta si el jugador está andando o no
    private bool walking = false;

    //Animators del player y de la pantalla de Transición
    public Animator playerAnimator;
    public Animator transitionAnimator;
    public Animator damageAnimator;



   

    //Referencia a la estela que deja el jugador
    public GameObject playerTrail;

    //referencia a los eefectos de particulas para el parry
    public VisualEffect projectilAbsortionRed;
    public VisualEffect projectilAbsortionGrey;

    //referencia a el efecto de la muerte del personaje
    public VisualEffect muerteParticles;

    //referencia al propio jugador y al cursor de apuntado
    public GameObject player;
    public GameObject firePointCursor;
    public GameObject informationBoxCollisionObject;

    //Rigibody del player
    private Rigidbody2D playerRigidbody;

    //Booleana para desactivar los controles
    public bool disabledControls = false;

    //Dimensiones para la caja de colisión del muñeco
    private Vector2 boxCheckRadius = new Vector2(0.98f, 1f);

    //Booleana para comprobar si el personaje está cayendo
    private bool imFallingInTheAir = false;

    //Lista de AudioSources del juego
    AudioSource[] AudioSources;

    public Enemy enemy;

    //Referencia a la flecha verde y a la pared de la derecha para las concidiones de victoria
    
    public GameObject rightStageCollision;

    public GameObject letterE;
    public GameObject flechaVerde;
    public GameObject AzulControles;
   

    public GameObject[] torretasConTag = new GameObject[0];


    //Animator del cartel
    public Animator menu;

    //Referencia a attack
    public GameObject rotation;
    public Attack attack;

    public bool deactivateE = false;
    //Booleana para activar el parry

    //Fix para impedir que la pantalla parpadee varias veces
    public bool deactivateDamageAnimationBool = false;

    public bool healTurrets = false;

    public Animator blueTextAnimator;
    public Animator blueScreenAnimator;

    public bool estoyEscondido = false;

    public IEnumerator doAParry()
    {
        if (unableToParry == false)
        {
            //Activamos el parry
            unableToParry = true;
            parryActivated = true;
            audioParry.Play();


            playerAnimator.SetBool("finParry", false);
          


            playerAnimator.SetTrigger("heHechoParry");

            //Desactivamos el efecto del parry
            yield return new WaitForSeconds(0.2f);
            parryActivated = false;
            playerAnimator.SetBool("finParry", true);

        
            

            //Esperamos X tiempo antes de poder volver a activar el parry
            yield return new WaitForSeconds(0.25F);

            //Volvemos a ser capaces de usar el parry
            unableToParry = false;
        }
        

    }

    public IEnumerator respawn()
    {
        life = 1;
        healTurrets = true;
        playerShoots = 0;
        disabledControls = true;
        deactivateE = true;
        audioMuerte.Play();
        muerteParticles.Play();

        menu.SetTrigger("muerte");

        attack.canFire = true;
       

        //Desactivamos la estela del jugador, su imagen y el cursor de apuntado
        playerTrail.GetComponent<TrailRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        firePointCursor.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;

        //Congelamos la posición del jugador
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

        //Desactivamos la colisión de la caja de información temporalmente para evitar animaciones inoportunas
        informationBoxCollisionObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1);

        //Activamos la Animación de transición tras morir
        transitionAnimator.SetBool("transicionActivada", true);
        transitionAnimator.SetTrigger("transicionCheck");

        yield return new WaitForSeconds(0.1f);

        //Poco después desactivamos la transición para evitar bucles
        transitionAnimator.SetBool("transicionActivada", false);

        yield return new WaitForSeconds(0.30f);

        //Devolvemos al jugador al origen del nivel y quitamos su velocidad
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        enemyScriptReference.timebetween = enemyScriptReference.starttimeb;
        letterE.SetActive(true);
        flechaVerde.SetActive(false);
        blueScreenAnimator.SetBool("pantallazo", false);
        blueTextAnimator.SetBool("pantallazoTexto", false);



        /*for (int i = 0; i < torretasConTag.Length; i++)
        {
            torretasConTag[i].SetActive(true);
        }*/

        foreach (GameObject Torreta in torretasConTag)
        {
            Torreta.SetActive(true);
            Torreta.GetComponent<SpriteRenderer>().enabled = true;
            Torreta.GetComponent<BoxCollider2D>().enabled = true;
        }
        enemy.morirUnaVez = false;
        enemy.playerDetected = true;
        healTurrets = false;


        //Reactivamos la imágenes
        playerTrail.GetComponent<TrailRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        firePointCursor.GetComponent <SpriteRenderer>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        informationBoxCollisionObject.GetComponent<BoxCollider2D>().enabled = true;

        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Devolvemos la vida al jugador
        disabledControls = false;
        deactivateE = false;
        yield return null;




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

        AudioSources = GetComponents<AudioSource>();
        audioSalto = AudioSources[0];
        audioMuerte = AudioSources[1];
        audioParry = AudioSources[2];
        audioDano = AudioSources[3];

        enemy = enemyModel.GetComponent<Enemy>();

        torretasConTag = torretasConTag.Concat(GameObject.FindGameObjectsWithTag("Turret")).ToArray();
        torretasConTag = torretasConTag.Concat(GameObject.FindGameObjectsWithTag("TurretDown")).ToArray();
        torretasConTag = torretasConTag.Concat(GameObject.FindGameObjectsWithTag("TurretRight")).ToArray();




        attack = rotation.GetComponent<Attack>();
    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            estoyEscondido = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            estoyEscondido = false;
        }
    }

   

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            life = life - 1;
            checkDamageStatus();

        }*/

        /*if (GameObject.FindGameObjectsWithTag("Turret").Length == 0)
        {
            
            rightStageCollision.SetActive(false);
        }

        if (GameObject.FindGameObjectsWithTag("TurretDown").Length == 0)
        {

            rightStageCollision.SetActive(false);
        }*/


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float Yspeed = rb.velocity.y;

        

        if(Yspeed <= -0.1f)
        {
            imFallingInTheAir = true;
        }
        else
        {
            imFallingInTheAir = false;
        }

        if(imFallingInTheAir == true)
        {

            playerAnimator.SetBool("estoyCayendo", true);

        }
        else
        {
            playerAnimator.SetBool("estoyCayendo", false);
        }

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


        
        if(disabledControls == false)
        {

            rb.velocity = new Vector2(speed * move, rb.velocity.y);


        }

        if (Input.GetButtonDown("Jump") && isGrounded == true && disabledControls == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));

            audioSalto.Play();

            //Indicamos al animator que hemos saltado
            playerAnimator.SetTrigger("heSaltado");

            


        }

        //Si pulsas el botón derecho del ratón, se hace un parry
        if (Input.GetMouseButtonDown(1) && disabledControls == false)
        {
            StartCoroutine(doAParry());
        }

        
        // Comprobamos la dirección de las pulsaciones
        float inputAxis = Input.GetAxisRaw("Horizontal");

        // Según la última tecla pulsada, activamos el flip o no
        if (inputAxis > 0 && disabledControls == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (inputAxis < 0 && disabledControls == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }

        bool allInactive = true;

        for (int i = 0; i < torretasConTag.Length; i++)
        {
            if (torretasConTag[i].activeInHierarchy)
            {
                allInactive = false;
                break;
            }
        }

        if (allInactive)
        {
            rightStageCollision.SetActive(false);
            flechaVerde.SetActive(true);
            AzulControles.SetActive(false);
        }
    }

    void GroundCheck()
    {

        Vector2 center = transform.position;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, boxCheckRadius, 0f, groundLayer);

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;

        }
    }

    //Método que hacer perder vida al jugador
    public void checkDamageStatus()
    {

        if(parryActivated == true)
        {
            if(playerShoots >= 3)
            {
                projectilAbsortionGrey.Play();
            }
            else
            {
                playerShoots = playerShoots + 1;
                projectilAbsortionRed.Play();
            }
        }
        else
        {

            if(life > 0)
            {
                life = life - 1;

                if (deactivateDamageAnimationBool == false)
                {

                    StartCoroutine(blockdDamageAnimation());

                }
                audioDano.Play();
            }
            else
            {
                
                StartCoroutine(respawn());
                if (deactivateDamageAnimationBool == false)
                {

                    StartCoroutine(blockdDamageAnimation());

                }
                letterE.SetActive(false);




            }
        }
    }

    public IEnumerator blockdDamageAnimation()
    {


        deactivateDamageAnimationBool = true;
        damageAnimator.SetTrigger("heSidoDanado");
        yield return new WaitForSeconds(0.85f);
        deactivateDamageAnimationBool = false;



    }
   
}
