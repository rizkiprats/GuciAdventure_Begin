using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChoiceBox
{
    [System.Serializable]
    public struct choiceBox
    {
        public bool Selected;
        [TextArea(2, 10)]
        public string Choice;
        public UnityEvent ChoiceEvent;
        
    }

    public choiceBox[] choiceBoxes= new choiceBox[3];
}