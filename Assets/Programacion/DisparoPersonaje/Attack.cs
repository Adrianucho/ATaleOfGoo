using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform firepoint; // referencia de adonde tiene que disparar 
    public GameObject goo;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //si pulsa el botón se ejecuta el disparo
        {
            Shoot();
        }



    }
    void Shoot ()
    {
        Instantiate(goo, firepoint.position, firepoint.rotation); //instancia la bala en donde le pide
    }
}
