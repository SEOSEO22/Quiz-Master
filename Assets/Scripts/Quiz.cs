using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

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
    

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
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
        SetButtonState(true);
        SetDefaultButtonSprites();
        DesplayQuestion();
    }

    // ������ �亯 �ؽ�Ʈ�� �����ϴ� �޼���
    void DesplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            answerText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = question.GetAnswer(i);
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

    // ������ �˷��ִ� �޼���
    void DisplayAnswer(int index)
    {
        answerIndex = question.GetCorrectAnswerIndex();
        buttonImage = answerButton[answerIndex].GetComponent<Image>();

        if (index == answerIndex)
        {
            questionText.text = "����!";
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "Ʋ�Ƚ��ϴ�!\n������ " + question.GetAnswer(answerIndex) + "�Դϴ�.";

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
