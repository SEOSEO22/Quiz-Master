using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButton;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;
    int answerIndex;
    Image buttonImage;
    TextMeshProUGUI answerText;

    private void Start()
    {
        GetNextQuestion();
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

    // ����/���� ���� �� ��ȣ�ۿ��� ��Ÿ���� �޼���
    public void OnAnswerSelected(int index)
    {
        answerIndex = question.GetCorrectAnswerIndex();
        buttonImage = answerButton[index].GetComponent<Image>();

        if (index == answerIndex)
        {
            questionText.text = "����!";
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "����!\n������ " + question.GetAnswer(answerIndex) + "�Դϴ�.";

            buttonImage.color = new Color32(255, 130, 130, 120);
            buttonImage = answerButton[answerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
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
        }
    }
}
