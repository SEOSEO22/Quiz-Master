using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer = 0;
    int questionsSeen = 0;

    public int getCorrectAnswers()
    {
        return correctAnswer;
    }

    public void setCorrectAnswers()
    {
        correctAnswer++;
    }

    public int getquestionsSeen()
    {
        return questionsSeen;
    }

    public void setquestionsSeen()
    {
        questionsSeen++;
    }

    public int CaculateScore()
    {
        return Mathf.RoundToInt(correctAnswer / (float)questionsSeen * 100);
    }
}
