using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject Projectile; // referencia del game object de la bala

    public Transform firepoint; // donde va a disparar

    float timebetween; // float del tiempo entre que dispara
    public float starttimeb; //le dice que empiece el tiempo que tiene que disparar

    public int health = 100; // vida del enemigo

    void Start()
    {
        timebetween = starttimeb;
    }

    // Update is called once per frame
    void Update()
    {
       if (timebetween <= 0) //si el tiempo entre que dispara es 0 instancia una bala en x posición
        {
            Instantiate(Projectile, firepoint.position, firepoint.rotation);
            timebetween = starttimeb;
        }
       else 
        {
            timebetween -= Time.deltaTime;
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
