using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
   public GameObject Projectile; // referencia del game object de la bala

    public Transform firepoint; // donde va a disparar

    public float timebetween; // float del tiempo entre que dispara
    public float starttimeb; //le dice que empiece el tiempo que tiene que disparar

    public int health = 100; // vida del enemigo
    private BoxCollider2D bCol2d;

    public bool playerDetected = true;
    public GameObject particleshoot;
    public int FramesToFlash = 1;
    //referencia a el efecto del aviso disparo
    public VisualEffect avisoDisparo;

    public VisualEffect DeathTurretParticles;

    //Declaro la 
    AudioSource audioDisparoTorreta, audioMuerteEnemigo, audioPredisparo, audioTorretaDanada;

    //Lista de AudioSources del juego
    AudioSource[] AudioSources;

    public AudioClip sonidoMuerteParaTorreta;

    //Referencia a la flecha verde y a la pared de la derecha para las concidiones de victoria
    public GameObject arrow;

    public Player player;
    public GameObject playermodel;

    public bool morirUnaVez = false;

    public GameObject prefabDeathSound;

    public Animator BlueScreenAnimator;
    public Animator BlueTextAnimator;

    IEnumerator enemyShoots()
    {
        playerDetected = false;
        audioPredisparo.Play();
        avisoDisparo.Play();
        yield return new WaitForSeconds(0.25f);

        particleshoot.SetActive(true);
        audioDisparoTorreta.Play();
        Instantiate(Projectile, firepoint.position, firepoint.rotation);
        yield return new WaitForSeconds(1);
        playerDetected = true;
        particleshoot.SetActive(false);

    }

    IEnumerator persistentTurret()
    {
        //Desactivamos el sprite renderer
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(prefabDeathSound, transform.position, transform.rotation);
        DeathTurretParticles.Play();

        Die();

        yield return null;
    }

    void Start()
    {
        
        bCol2d = GetComponent<BoxCollider2D>();
        AudioSources = GetComponents<AudioSource>();
        audioDisparoTorreta = AudioSources[0];
        audioMuerteEnemigo= AudioSources[1];
        audioPredisparo = AudioSources[2];
        audioTorretaDanada = AudioSources[3];

        player = playermodel.GetComponent<Player>();


    }

    // Update is called once per frame
    void Update()
    {

        if (player.healTurrets == true)
        {

            health = 40;
        }


        if (gameObject.CompareTag("Turret"))
        {
            //Length of the ray
            float laserLength = 10f;
            //Start point of the laser
            Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
            int layerMask = LayerMask.GetMask("Default");
            //Check if the laser hit something
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.left, laserLength, layerMask);

            if (hit.collider.tag == "Player")
            {
                if (playerDetected == true)
                {


                    StartCoroutine(enemyAttacks());


                    StartCoroutine(enemyShoots());
                    //StartCoroutine(DoFlash());
                }



            }
        }

        if (gameObject.CompareTag("TurretDown"))
        {
            //Length of the ray
            float laserLength = 10f;
            //Start point of the laser
            Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
            int layerMask = LayerMask.GetMask("Default");
            //Check if the laser hit something
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, laserLength, layerMask);

            if (hit.collider.tag == "Player")
            {
                if (playerDetected == true && health > 0)
                {


                    StartCoroutine(enemyAttacks());


                    StartCoroutine(enemyShoots());
                    //StartCoroutine(DoFlash());
                }



            }

            if(hit.collider.tag == "Turret")
            {

                
                if (player.disabledControls == false && player.estoyEscondido == true)
                {

                    StartCoroutine(enemyShoots());
                    BlueScreenAnimator.SetBool("pantallazo", true);
                    BlueTextAnimator.SetBool("pantallazoTexto", true);



                }


            }

        }

        if (gameObject.CompareTag("TurretRight"))
        {
            //Length of the ray
            float laserLength = 12f;
            //Start point of the laser
            Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
            int layerMask = LayerMask.GetMask("Default");
            //Check if the laser hit something
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, laserLength, layerMask);

            if (hit.collider.tag == "Player")
            {
                if (playerDetected == true && health > 0)
                {


                    StartCoroutine(enemyAttacks());


                    StartCoroutine(enemyShoots());
                    //StartCoroutine(DoFlash());
                }



            }
        }

     



    }

    

    public void TakeDamage (int damage) //metodo que quita vida del enemigo
    {
        audioTorretaDanada.Play();
        health -= damage;
        if (health<=0) // si la salud llega a 0 muere
        {
            StartCoroutine(persistentTurret());
            
        }
    }

    /*IEnumerator DoFlash()
    {
        particleshoot.SetActive(true);
        var framesFlashed = 0;
        
        while (framesFlashed <= FramesToFlash)
        {
            

            yield return null;
        }
        particleshoot.SetActive(false);
        

    }*/

    public IEnumerator enemyAttacks()
    {
        
        avisoDisparo.Play();
        yield return new WaitForSeconds(0.25f);
        

    }

    public IEnumerator recoverHealth()
    {

        
        yield return new WaitForSeconds(0.25f);
        player.healTurrets = false;
        yield return null;
    }

    public void Die() //metodo para que se ejecute la muerte
    {
        playerDetected = true;
        health = 40;
        avisoDisparo.Stop();
        this.gameObject.SetActive(false); // se destruye el enemigo
        
    }
  
}
