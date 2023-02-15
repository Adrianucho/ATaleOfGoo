using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue 
{
    public string name; // array para los nombres de los personajes
    [TextArea(3, 10)]
    public string[] sentences; 


}
