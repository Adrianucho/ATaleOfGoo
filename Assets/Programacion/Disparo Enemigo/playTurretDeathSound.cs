using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTurretDeathSound : MonoBehaviour
{
    IEnumerator waitAndExplode()
    {

        yield return new WaitForSeconds(5);
        Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitAndExplode());
    }
}
