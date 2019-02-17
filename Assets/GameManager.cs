using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;
    private static int questionCounter = 0;
    private static int correctCounter = 0;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private float timeBetweenQuestions = 0.3f;

    [SerializeField]
    private Animator animator;

    void Start()
    {

        if(unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        Debug.Log("Counter: " + questionCounter);

        SetCurrentQuestion();
        questionCounter += 1;

        if(questionCounter == 10)
        {
            Debug.Log("I should stop running this crap!");
            ScreenSwitched();
        }



    }

    public void ScreenSwitched()
    {
        if(correctCounter >= 8)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    void SetCurrentQuestion()
    {

        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "Good Choice!";
            falseAnswerText.text = "You Serious??";
        }
        else
        {
            trueAnswerText.text = "WRONGGGG!!";
            falseAnswerText.text = "Wise Decision!";
        }

    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if(currentQuestion.isTrue){
            correctCounter += 1;
            Debug.Log("Correct");
        }else{
            Debug.Log("Wrong!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }
}
