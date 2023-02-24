using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour //script que triggea la conversación
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //busca al dialogue manager y hace que empieze la conver
    }

    void OnTriggerEnter2D(Collider2D col) //si collisiona con el jugador se destruye
    {
        if (col.gameObject.tag == "Goo")
        {
            Debug.Log("Le ha dado");
            //El juego comprueba si hacer daño o no
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //busca al dialogue manager y hace que empieze la conver

         

        }
    }
}
