using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{


    public Text nameText;
    public Text dialogueText;
    public Animator menu;
    public GameObject rotation;
    public Attack attack;



    private Queue<string> sentences; //array de las frases

    public Player playerScript;
    public GameObject playerGameobject;

    public bool blockiIsClose = false;

    void Start()
    {
        sentences = new Queue<string>();
        attack = rotation.GetComponent<Attack>();
        playerScript = playerGameobject.GetComponent<Player>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        blockiIsClose = false;
        attack.canFire = false;
        playerScript.deactivateE = true;
        nameText.text = dialogue.name;

        sentences.Clear(); // limpia las frases en el array

        foreach (string sentence in dialogue.sentences) //hacemos que salga la siguiente frase
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(); //pasa a la siguiente frase
    }

    public void DisplayNextSentence() // metodo para cambiar de frase
    {


        if (sentences.Count == 0)
        {

            attack.canFire = true;
            playerScript.deactivateE = false;
            EndDialogue(); //termina el dialogo
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence) //esto srive para que en el dialogue box vayan apareciendo letras poco a poco
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {

        if(blockiIsClose == false)
        {

            StartCoroutine(closeDialogueBox());


        }
    }

    IEnumerator closeDialogueBox()
    {
        blockiIsClose = true;
        menu.SetTrigger("IsClose");
        yield return null;

    }


}

