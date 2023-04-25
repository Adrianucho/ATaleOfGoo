using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform objetoSeguido;
    public float velocidad = 1.0f;
    public float velocidadVuelta = 1.0f;

    public GameObject torretaTecho;
    private bool seguir = false;

    public Player player;
    public GameObject playerModel;

    public Vector2 posicionInicial;


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
        Debug.Log(player.disabledControls);
        if (seguir == true)
        {
            // Copiar la posición del objeto seguido en el eje X
            float nuevaPosicionX = objetoSeguido.transform.position.x;

            // Mover el objeto seguidor a la nueva posición en el eje X
            Vector3 nuevaPosicion = torretaTecho.transform.position;
            nuevaPosicion.x = nuevaPosicionX;
            torretaTecho.transform.position = Vector3.Lerp(torretaTecho.transform.position, nuevaPosicion, velocidad * Time.deltaTime);
        }

        if(player.disabledControls == true)
        {

            torretaTecho.transform.position = Vector3.Lerp(torretaTecho.transform.position, posicionInicial, velocidad * Time.deltaTime);

        }

    }
}
