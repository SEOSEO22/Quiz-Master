using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
    int answerIndex;
    bool hasAnsweredEarly;
    TextMeshProUGUI answerText;

    [Header("Button")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;
    Image buttonImage;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    // 다음 질문을 설정하는 메서드
    void GetNextQuestion()
    {
        if (questions.Count != 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();

            GetRandomQuestion();
            DesplayQuestion();
            scoreKeeper.setquestionsSeen();
        }
    }

    // 리스트 내 질문을 랜덤으로 선별하는 메서드
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    // 질문과 답변 텍스트를 설정하는 메서드
    void DesplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            answerText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = currentQuestion.GetAnswer(i);
        }
    }

    // 답변 버튼 선택 시 상호작용을 나타내는 메서드
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetButtonState(false);
        timer.cancleTimer();
    }

    // 정답 여부를 알려주는 메서드
    void DisplayAnswer(int index)
    {
        answerIndex = currentQuestion.GetCorrectAnswerIndex();
        buttonImage = answerButton[answerIndex].GetComponent<Image>();

        if (index == answerIndex)
        {
            questionText.text = "정답!";
            scoreKeeper.setCorrectAnswers();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "틀렸습니다!\n정답은 " + currentQuestion.GetAnswer(answerIndex) + "입니다.";

            if (index != -1)
            {
                buttonImage.sprite = correctAnswerSprite;
                buttonImage = answerButton[index].GetComponent<Image>();
                buttonImage.color = new Color32(255, 130, 130, 120);
            }
            else
            {
                buttonImage.sprite = correctAnswerSprite;
            }
        }

        scoreText.text = "Score : " + scoreKeeper.CaculateScore() + "%";
    }

    // 버튼을 비활성화 상태로 만드는 메서드
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    // 답변 버튼의 스프라이트를 기본 스프라이트로 세팅하는 메서드
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            buttonImage = answerButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
            buttonImage.color = new Color32(255, 255, 255, 255);
        }
    }
}
