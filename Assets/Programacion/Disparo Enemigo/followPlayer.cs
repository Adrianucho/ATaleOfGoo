using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform objetoSeguido;
    public Transform seguimientoParticulas;
    public float velocidad = 1.0f;
    public float velocidadVuelta = 1.0f;

    public GameObject torretaTecho;
    private bool seguir = false;

    public Player player;
    public GameObject playerModel;

    public Vector2 posicionInicial;

    public Animator OverloadTextAnimator;
    public Animator OverloadScreenAnimator;



    private void Start()
    {
        player = playerModel.GetComponent<Player>();
        posicionInicial = torretaTecho.transform.position;

    }

    void OnTriggerEnter2D(Collider2D objetoConCollision)
    {
        if (objetoConCollision.gameObject.CompareTag("Player"))
        {
            seguir = true;

        }
    }

    void OnTriggerExit2D(Collider2D objetoConCollision)
    {
        if (objetoConCollision.gameObject.CompareTag("Player"))
        {
            seguir = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (seguir == true)
        {
            // Copiar la posición del objeto seguido en el eje X
            float nuevaPosicionX = objetoSeguido.transform.position.x;
            float nuevaPosicionx2 = objetoSeguido.transform.position.x;

            // Mover el objeto seguidor a la nueva posición en el eje X
            Vector3 nuevaPosicion = torretaTecho.transform.position;
            nuevaPosicion.x = nuevaPosicionX;
            torretaTecho.transform.position = Vector3.Lerp(torretaTecho.transform.position, nuevaPosicion, velocidad * Time.deltaTime);

            if (torretaTecho.activeSelf)
            {

                Vector3 nuevaPosicion2 = seguimientoParticulas.transform.position;
                nuevaPosicion2.x = nuevaPosicionx2;
                seguimientoParticulas.transform.position = seguimientoParticulas.transform.position = nuevaPosicion2;


            }
        

            

        }

        if (player.disabledControls == true)
        {

            torretaTecho.transform.position = Vector3.Lerp(torretaTecho.transform.position, posicionInicial, velocidad * Time.deltaTime);

        }


    }

    public IEnumerator cambiarPosicionParticulas()
    {

        seguimientoParticulas.transform.position = torretaTecho.transform.position;
        yield return null;

    }

}