using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButton;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;
    int answerIndex;
    TextMeshProUGUI answerText;

    private void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            answerText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = question.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        answerIndex = question.GetCorrectAnswerIndex();
        Image buttonImage = answerButton[index].GetComponent<Image>();

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
    }
}
