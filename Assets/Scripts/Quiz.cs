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

    // 다음 질문을 설정하는 메서드
    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DesplayQuestion();
    }

    // 질문과 답변 텍스트를 설정하는 메서드
    void DesplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            answerText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = question.GetAnswer(i);
        }
    }

    // 정답/오답 선택 시 상호작용을 나타내는 메서드
    public void OnAnswerSelected(int index)
    {
        answerIndex = question.GetCorrectAnswerIndex();
        buttonImage = answerButton[index].GetComponent<Image>();

        if (index == answerIndex)
        {
            questionText.text = "정답!";
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "오답!\n정답은 " + question.GetAnswer(answerIndex) + "입니다.";

            buttonImage.color = new Color32(255, 130, 130, 120);
            buttonImage = answerButton[answerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
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
        }
    }
}
