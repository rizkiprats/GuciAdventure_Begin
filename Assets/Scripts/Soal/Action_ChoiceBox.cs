using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Action_ChoiceBox : MonoBehaviour
{
    [TextArea(2, 10)]
    public string ChoiceSentence;
    public ChoiceBox choiceBox;

    [Header("ChoiceBox")]
    public TMP_Text ChoiceText;
    public List<Button> ButtonChoice;
    public int Number;

    private void Start()
    {
        ChoiceText.text = ChoiceSentence.ToString();

        RandomizeChoice();
    }

    private void Update()
    {
        foreach(Button btn in ButtonChoice)
        {
            btn.onClick.AddListener(delegate { btn.interactable = false; });
        }
    }

    public void RandomizeChoice()
    {
        if(ButtonChoice.Count != 0 && choiceBox.choiceBoxes.Length != 0)
        {
            List<int> randomChoices = new List<int>();

            for (int i = 0; i < ButtonChoice.Count; i++)
            {
                int randomChoicesIndex = Random.Range(0, choiceBox.choiceBoxes.Length);
                while (randomChoices.Contains(randomChoicesIndex))
                {
                    randomChoicesIndex = Random.Range(0, choiceBox.choiceBoxes.Length);
                }
                if (ButtonChoice[i].GetComponentInChildren<TMP_Text>() != null)
                {
                    ButtonChoice[i].GetComponentInChildren<TMP_Text>().text = choiceBox.choiceBoxes[randomChoicesIndex].Choice;
                }
                if (ButtonChoice[i].GetComponent<Add_SoalEntry>() != null)
                {
                    ButtonChoice[i].GetComponent<Add_SoalEntry>().setJawaban(choiceBox.choiceBoxes[randomChoicesIndex].Selected, Number);
                }
                ButtonChoice[i].onClick.AddListener(choiceBox.choiceBoxes[randomChoicesIndex].ChoiceEvent.Invoke);
                ButtonChoice[i].onClick.AddListener(delegate {setChoiceSelected(randomChoicesIndex, true);} );
                ButtonChoice[i].onClick.AddListener(delegate { Debug.Log(randomChoicesIndex); });
                randomChoices.Add(randomChoicesIndex);
            }
        }
    }

    private void setChoiceSelected(int index, bool value)
    {
        choiceBox.choiceBoxes[index].Selected = value;
    }

}