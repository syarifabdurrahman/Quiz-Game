using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region VariableQuestion
    public QuestionClass[] category;
    [HideInInspector]
    public Question[] thisQuestions;
    public bool changeLevel;
    private List<Question> unansweredQuestion;
    private Question currentQuestion;

    [SerializeField]
    private Text FactText;

    [SerializeField]
    private Text TrueAnswerText;

    [SerializeField]
    private Text FalseAnswerText;

    [SerializeField]
    private Animator[] animator;

    [SerializeField]
    private float TimeBetweenQuestions = 1f;
    public int PLayerTrueAnswer;
    public int PlayerMaxTrueAnswer;
    #endregion
    #region StatsVariable
    public int Playermaxhealth;
    public int PlayerCurrentHealth = 100;

    #endregion
    public bool Isbattle = false;
    private int count = 0;

    public GameObject PausePanel;
    public GameObject DeadPanel;
    public GameObject HealthBar;
    public GameObject PauseButton;
    public GameObject Objective;
    public GameObject OptionPanel;

    public static GameManager Instance { set; get; }


    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        if (unansweredQuestion == null || unansweredQuestion.Count == 0)
        {
            
            thisQuestions = category[0].questions;
            unansweredQuestion = new List<Question>(thisQuestions);
            //unansweredQuestion = new List<Question>(questions);
        }

        setCurrentQuestion();


    }
    #region MainQuestion


    void setCurrentQuestion()
    {

        int RandomQuestionIndex = Random.Range(0, unansweredQuestion.Count);
        currentQuestion = unansweredQuestion[RandomQuestionIndex];
        FactText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            
            Isbattle = true;
            TrueAnswerText.text = "CORRECT";
            FalseAnswerText.text = "WRONG!";
        }
        else
        {
     
            Isbattle = false;
            TrueAnswerText.text = "WRONG!";
            FalseAnswerText.text = "CORRECT";
        }

    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestion.Remove(currentQuestion);

        yield return null;

        if (unansweredQuestion.Count == 0 || unansweredQuestion == null)
        {
            count = 0;
            changeLevel = false;
            thisQuestions = category[0].questions;
            unansweredQuestion = new List<Question>(thisQuestions);
            //unansweredQuestion = new List<Question>(questions);
            count++;
            if (count == 5) ;
            {
                changeLevel = true;
                Objective.SetActive(true);
                StartCoroutine(EveryChapter());
            }
        }
        setCurrentQuestion();
    }

    IEnumerator EveryChapter()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    public void HandleAnswerButton(bool answer)
    {
        if (answer)
        {
            animator[0].SetTrigger("click");

            if (currentQuestion.isTrue)
            {

                Debug.Log("Correct");
            }
            else
            {
                Debug.Log("Wrong!");
            }
            StartCoroutine(TransitionToNextQuestion());
        }

        if (!answer)
        {

            animator[1].SetTrigger("Clack");
            Isbattle = true;
            if (!currentQuestion.isTrue)
            {
                Debug.Log("Wrong!");
            }
            else
            {
                Debug.Log("Correct");
            }
            StartCoroutine(TransitionToNextQuestion());
        }

    }
    #endregion


    #region Panels
    public void Paused()
    {
        StartCoroutine(Pause());
        PausePanel.SetActive(true);
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(.3f);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void Dead()
    {
        DeadPanel.SetActive(true);
        HealthBar.SetActive(false);
        PauseButton.SetActive(false);
    }

    public void TheOptionPanel()
    {
        OptionPanel.SetActive(true);
    }


    #endregion

    #region TransitionToNextScene
    public void ToTheTown()
    {
        SceneManager.LoadScene(0);
    }


    public void Load(string SceneName)
    {
        if (!SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

    }

    public void Unload(string SceneName)
    {
        if (SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            SceneManager.UnloadScene(SceneName);
        }
    }
    #endregion
}
