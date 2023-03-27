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

    private bool playerDetected = true;
    public GameObject particleshoot;
    public int FramesToFlash = 1;
    //referencia a el efecto del aviso disparo
    public VisualEffect avisoDisparo;

    //Declaro la 
    AudioSource audioDisparoTorreta, audioMuerteEnemigo, audioPredisparo;

    //Lista de AudioSources del juego
    AudioSource[] AudioSources;

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


    void Start()
    {
        //
        bCol2d = GetComponent<BoxCollider2D>();
        AudioSources = GetComponents<AudioSource>();
        audioDisparoTorreta = AudioSources[0];
        audioMuerteEnemigo= AudioSources[1];
        audioPredisparo = AudioSources[2];

    }

    // Update is called once per frame
    void Update()
    {
        //Length of the ray
        float laserLength = 10f;
        //Start point of the laser
        Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
        int layerMask = LayerMask.GetMask("Default");
        //Check if the laser hit something
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.left, laserLength, layerMask);

      if (hit.collider.tag == "Player" )
        {
            if (playerDetected == true)
            {
                StartCoroutine(enemyAttacks());


                StartCoroutine(enemyShoots());
                //StartCoroutine(DoFlash());
            }
            

        }

        


    }

    public void TakeDamage (int damage) //metodo que quita vida del enemigo
    {
        health -= damage;
        if (health<=0) // si la salud llega a 0 muere
        {
            audioMuerteEnemigo.Play();
            Die();
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

    

    public void Die() //metodo para que se ejecute la muerte
    {
        
        Destroy(gameObject); // se destruye el enemigo
    }
  
}
