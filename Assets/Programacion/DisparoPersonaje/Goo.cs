using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb; //referencia del rigid body
    void Start()
    {
        rb.velocity = transform.right * speed; // le dice adonde tiene que ir
    }

    void OnTriggerEnter2D(Collider2D hitInfo) //si collisiona con el jugador se destruye
    {
        Destroy(gameObject);
    }
}
