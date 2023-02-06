using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject Projectile;

    float shootRate;
    float shootagain;

    void Start()
    {
        shootRate = 3f;
        shootagain = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        lookiftimetoshoot();
    }


   public void lookiftimetoshoot()
    {
        if (Time.time > shootagain)
        {
            Instantiate(Projectile, transform.position, Quaternion.identity);
            shootagain = Time.time + shootRate;

        }
    }
}
