using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    public List<Question> questionList = new List<Question>();

    //[HideInInspector]
    public List<Question> questionStack = new List<Question>();

    
    void Awake()
    {
        //Prevention in case another instance would be created
        if (!instance) {
            instance = this;
            Debug.Log("Set instance " + " GameManager");
        }
        else {
            Destroy(this);
        }

    }

    
    void Update()
    {
        
    }

    public void ShufflesStack() {
        questionStack = questionList.OrderBy(x => Random.value).ToList();
    }
}
