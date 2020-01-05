using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public Text questionDisplayText;
    public Text explanationDisplayText;
    public Text scoreDisplayText;
    public Text highScoreDisplay;

    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionPanel;
    public GameObject roundEndPanel;
    public GameObject explanationPanel;
    public GameObject explanationButton;
    public GameObject nextButton;
    public AudioClip correctAnswer, incorrectAnswer;
    public Sprite musicOn;
    public Sprite musicOff;
    public Button musicBtn;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;
    private AudioSource myAudioSource;
    private bool musicPlaying = true;
    private bool canAnswer = true;

    private int questionIndex;
    private int playerScore;
    public int quizLength = 50;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private int j = 0; //initialize quiz count

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        myAudioSource = GetComponent<AudioSource>();

        roundEndPanel.SetActive(false);
        explanationPanel.SetActive(false);

        playerScore = 0;
        ShowQuestion();
    }


    private void ShowQuestion()
    {
        questionPanel.SetActive(true);
        nextButton.SetActive(false);
        explanationButton.SetActive(false);

        questionIndex = Random.Range(0, questionPool.Length);

        RemoveAnswerButtons();
        canAnswer = true;

        QuestionData questionData = questionPool[questionIndex]; 
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {   //for every AnswerData in current QuestionData, spawn an AnswerButton from the object pool
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            //passing AnswerData to ANswerButton so it knows what text to display and whether it's the correct answer
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.GetComponent<Image>().color = new Color32 (255, 255, 255, 50);
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {   //Return all spawned AnswerButtons to the object pool
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if(canAnswer)
        {
            explanationButton.SetActive(true);
            nextButton.SetActive(true);

            if (isCorrect)
            {
                myAudioSource.PlayOneShot(correctAnswer);
                playerScore += currentRoundData.pointsAddedForCorrectAnswer;
                scoreDisplayText.text = "Score: " + playerScore.ToString() + "%";
            }
            else if (!isCorrect)
            {
                myAudioSource.PlayOneShot(incorrectAnswer);
            }

            QuestionData questionData = questionPool[questionIndex];
            GameObject[] answerBtns = GameObject.FindGameObjectsWithTag("AnswerButton");

            foreach (GameObject answer in answerBtns)
            {   //check child text if its true etc
                bool correctAnswer = answer.GetComponent<AnswerButton>().answerData.isCorrect;

                if (correctAnswer)
                {
                    answer.GetComponent<Image>().color = new Color32(146, 234, 79, 155); //Color.green;

                }
                else
                {
                    answer.GetComponent<Image>().color = new Color32(236, 84, 79, 155);
                }
            }
            canAnswer = false;
        }
        
    }

    public void ShowExplanationPanel()
    {   
        questionPanel.SetActive(false);
        explanationButton.SetActive(false);
        explanationPanel.SetActive(true);
        nextButton.SetActive(true);

        QuestionData questionData = questionPool[questionIndex]; 
        explanationDisplayText.text = questionData.explanationText;
    }

    public void MovingOn()
    {
        explanationPanel.SetActive(false);
        j++;

        if (j < quizLength)
        {
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        nextButton.SetActive(false);
        explanationButton.SetActive(false);

        dataController.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = "Hi-score: " + dataController.GetHighestPlayerScore().ToString() + "%";

        questionPanel.SetActive(false);
        explanationPanel.SetActive(false);
        roundEndPanel.SetActive(true);
    }

    public void musicController()
    {
        if(musicPlaying)
        {
            AudioListener.volume = 0.0f;
            musicBtn.image.sprite = musicOff;
            musicPlaying = false;
        }
        else if(!musicPlaying)
        {
            AudioListener.volume = 1.0f;
            musicBtn.image.sprite = musicOn;
            musicPlaying = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void ResetScore()
    {
        dataController.SubmitNewPlayerScore(120);
        highScoreDisplay.text = "Hi-score: 0";
    }

    public void ExitApp()
    {
        Application.Quit();
    }

}
