using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb; //referencia del rigid body
    public int damage = 20; //daño que hace el personaje
    void Start()
    {
        rb.velocity = transform.right * speed; // le dice adonde tiene que ir
    }

    void OnTriggerEnter2D(Collider2D hitInfo) //si collisiona con el jugador se destruye
    {
       Enemy enemy = hitInfo.GetComponent<Enemy>(); //coge el componente del enemigo
       if (enemy != null) // queremos ver si hemos encontrado el enemigo
        {
            enemy.TakeDamage(damage); // quitamos vida 

        }
        Destroy(gameObject);
    }
}
