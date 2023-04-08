using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform firepoint; // referencia de adonde tiene que disparar 
    public GameObject goo;

    private Camera mainCam;
    private Vector3 mousePos;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    //Referencia al sript de Player
    private Player PlayerScript;

    public GameObject PlayerFigure;

    //Partículas decorativas para disparoJugador
    public ParticleSystem playerShootDecorationParticles;
    //Declaro la 
    AudioSource audioDisparoSlime;
    //Lista de AudioSources del juego
    AudioSource[] AudioSources;


    private void Awake()
    {
        //Cogemos el script de Player de la figura del jugador
        PlayerScript = PlayerFigure.GetComponent<Player>();

    }

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        AudioSources = GetComponents<AudioSource>();
        audioDisparoSlime = AudioSources[0];

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); //sirve para que gracias a la camara pueda rotar para disparar

        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);



        if (Input.GetButtonDown("Fire1") && PlayerScript.disabledControls == false) //si pulsa el botón se ejecuta el disparo
        {


            //Si tenemos munición, disparamos
            if (PlayerScript.playerShoots > 0 && canFire == true)
            {
                audioDisparoSlime.Play();
                Shoot();


            }


        }



    }
    void Shoot()
    {
        playerShootDecorationParticles.Play();
        Instantiate(goo, firepoint.position, firepoint.rotation); //instancia la bala en donde le pide
        //Reducimos en uno la munición
        PlayerScript.playerShoots = PlayerScript.playerShoots - 1;
    }

}
