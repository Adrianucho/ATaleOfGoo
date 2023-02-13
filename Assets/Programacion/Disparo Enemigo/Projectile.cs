using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f; //variable de la velocidad de la bala

    Rigidbody2D rb; //referencia del rigid body

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //coge el componente del rigid body
        rb.velocity = transform.right * speed; // le dice adonde tiene que ir
        


    }

     void OnTriggerEnter2D(Collider2D col) //si collisiona con el jugador se destruye
    {
        if (col.gameObject.tag == "Player") 
        {
            Debug.Log("Le ha dado");
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Floor") 
        {
            
            Destroy(gameObject);
        }
    }

}
