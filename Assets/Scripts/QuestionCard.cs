using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionCard : MonoBehaviour
{
    public TMP_Text questionText;
    public Button btn_true, btn_false;

    public Color GrayedColor;
    public GameObject resultX, resultCheckmark;

    public ColorBlock cb_false, cb_true;

    List<Button> answerButtons = new List<Button>();


    private void Start() {
        cb_false = btn_false.colors;
        cb_true = btn_true.colors;
    }

    public void HighlightGivenAnswer(int index) {
        //Make other buttons execept given answer gray.
        for(int i= 0; i < answerButtons.Count - 1; i++) {
            if(i != index) {
                ColorBlock colorBlock = answerButtons[i].colors;
                colorBlock.normalColor = GrayedColor;
                answerButtons[i].colors = colorBlock;
            }
        }
    }

    public void ResetCard() {
        btn_false.colors = cb_false;
        btn_true.colors = cb_true;
    }

    public void ShowResult(bool correct) {
        resultCheckmark.SetActive(correct);
        resultX.SetActive(!correct);
    }

    public void HideResult() {
        resultCheckmark.SetActive(false);
        resultX.SetActive(false);
    }
}
