using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    public Question currentQuestion;
    public QuestionCard questionCard;

    //preset list
    public List<Question> questionList = new List<Question>();
    //[HideInInspector]
    //Active list
    public List<Question> questionStack = new List<Question>();

    GameLogic gameLogic;
    
    void Awake()
    {
        //Prevention in case another instance would be created
        if (!instance) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        gameLogic = GameLogic.instance;
    }

    /// <summary>
    /// This Method is referenced by the UI button from within the scene.
    /// </summary>
    /// <param name="index"></param>
    public void SubmitAnswer(int index) {
        gameLogic.SubmitAnswer(index == currentQuestion.correctAnswer, index);
        questionCard.HighlightGivenAnswer(index);
    }

    public void ShowQuestion() {
        currentQuestion = questionList[0];
        questionList.RemoveAt(0);
        questionCard.gameObject.SetActive(true);
        questionCard.questionText.text = currentQuestion.question;
    }

    public void HideQuestion() {
        questionCard.gameObject.SetActive(false);
    }

    public void ShufflesStack() {
        //clone and shuffle the preset question list
        questionStack = new List<Question>(questionList);
        questionStack.Shuffle();
    }

    public void ShowResult(bool correct) {
        questionCard.ShowResult(correct);
    }

    public void HideResult() {
        questionCard.HideResult();
    }

    
}
