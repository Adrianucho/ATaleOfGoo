using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

  
    public Text nameText;
    public Text dialogueText;
    public Animator menu;

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
      

        if (sentences.Count == 0)
        {
            Debug.Log("awa");
            EndDialogue(); //termina el dialogo
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence (string sentence) //esto srive para que en el dialogue box vayan apareciendo letras poco a poco
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
        Debug.Log("Se termino");
        menu.SetTrigger("IsClose");
    }

}
