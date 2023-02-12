using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue <string> sentences; //array de las frases

    void Start()
    {
        sentences = new Queue<string>();


    }

}
