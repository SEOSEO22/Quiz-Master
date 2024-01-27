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

    // ���� ������ �����ϴ� �޼���
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

    // ����Ʈ �� ������ �������� �����ϴ� �޼���
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    // ������ �亯 �ؽ�Ʈ�� �����ϴ� �޼���
    void DesplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            answerText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = currentQuestion.GetAnswer(i);
        }
    }

    // �亯 ��ư ���� �� ��ȣ�ۿ��� ��Ÿ���� �޼���
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetButtonState(false);
        timer.cancleTimer();
    }

    // ���� ���θ� �˷��ִ� �޼���
    void DisplayAnswer(int index)
    {
        answerIndex = currentQuestion.GetCorrectAnswerIndex();
        buttonImage = answerButton[answerIndex].GetComponent<Image>();

        if (index == answerIndex)
        {
            questionText.text = "����!";
            scoreKeeper.setCorrectAnswers();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "Ʋ�Ƚ��ϴ�!\n������ " + currentQuestion.GetAnswer(answerIndex) + "�Դϴ�.";

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

    // ��ư�� ��Ȱ��ȭ ���·� ����� �޼���
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    // �亯 ��ư�� ��������Ʈ�� �⺻ ��������Ʈ�� �����ϴ� �޼���
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
