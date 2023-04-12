using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaFinal : MonoBehaviour
{

    public void changescene(string Tutorial)
    {
        SceneManager.LoadScene("Tutorial2");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("HastaLueguito");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("EscenaFinal");
        }

    }
}
