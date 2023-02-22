using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject Projectile; // referencia del game object de la bala

    public Transform firepoint; // donde va a disparar

    public float timebetween; // float del tiempo entre que dispara
    public float starttimeb; //le dice que empiece el tiempo que tiene que disparar

    public int health = 100; // vida del enemigo
    private BoxCollider2D bCol2d;

    private bool ableToShoot = true;

    IEnumerator enemyShoots()
    {
        ableToShoot = false;
        Instantiate(Projectile, firepoint.position, firepoint.rotation);
        yield return new WaitForSeconds(1);
        ableToShoot = true;

    }


    void Start()
    {
        //
        bCol2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Length of the ray
        float laserLength = 5f;
        //Start point of the laser
        Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
        int layerMask = LayerMask.GetMask("Default");
        //Check if the laser hit something
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.left, laserLength, layerMask);

       
      if (hit.collider.tag == "Player" )
        {
            if(ableToShoot == true)
            {
                StartCoroutine(enemyShoots());
            }

        }

        


    }

    public void TakeDamage (int damage) //metodo que quita vida del enemigo
    {
        health -= damage;
        if (health<=0) // si la salud llega a 0 muere
        {
            Die();
        }
    }


    public void Die() //metodo para que se ejecute la muerte
    {
        Destroy(gameObject); // se destruye el enemigo
    }
  
}
