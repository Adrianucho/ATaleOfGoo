using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject Projectile; // referencia del game object de la bala

    public Transform firepoint; // donde va a disparar

    float timebetween; // float del tiempo entre que dispara
    public float starttimeb; //le dice que empiece el tiempo que tiene que disparar

   

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


  
}
