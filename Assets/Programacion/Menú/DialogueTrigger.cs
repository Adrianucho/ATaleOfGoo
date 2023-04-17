using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour //script que triggea la conversación
{
    public Dialogue dialogue;
    public Animator menu;
    public GameObject rotation;
    public Attack attack;
    public GameObject letrae;

    public Player playerScript;
    public GameObject playerCharacter;

    void Start()
    {
        attack = rotation.GetComponent<Attack>();
        playerScript = playerCharacter.GetComponent<Player>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //si pulsas letra se se abre el cuadro de dialogo
        {

            if(playerScript.deactivateE == false)
            {
                // attack.canFire = false;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //busca al dialogue manager y hace que empieze la conver
                menu.SetTrigger("IsOpen");
                letrae.SetActive(false);
            }
           

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

