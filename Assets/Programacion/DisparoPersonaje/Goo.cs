using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb; //referencia del rigid body
    public int damage = 20; //daño que hace el personaje

    private Camera mainCam;
    private Vector3 mousePos;
    public float force;

    void Start()
    {
     /*   rb.velocity = transform.right * speed; // le dice adonde tiene que ir
     */
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
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
