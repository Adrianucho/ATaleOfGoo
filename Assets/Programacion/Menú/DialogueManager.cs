using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

   
    public Text nameText;
    public Text dialogueText;

    
    private Queue <string> sentences; //array de las frases

    void Start()
    {
        sentences = new Queue<string>();


    }

    public void StartDialogue (Dialogue dialogue)
    {

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
        if (sentences.Count == 0 )
        {
            EndDialogue(); //termina el dialogo
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("Final de la conversación");
    }

}
