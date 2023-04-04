using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour //script que triggea la conversación
{
    public Dialogue dialogue;
    public Animator menu;
   
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //si pulsas letra se se abre el cuadro de dialogo
        {
            
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //busca al dialogue manager y hace que empieze la conver
            menu.SetTrigger("IsOpen");
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //busca al dialogue manager y hace que empieze la conver
    }

    
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goo"))
        {
            Debug.Log("Peloso");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //busca al dialogue manager y hace que empieze la conver
            menu.SetTrigger("IsOpen");
        }
    } */
}
