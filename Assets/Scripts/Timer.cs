using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 20f;
    [SerializeField] float timeToShowAnswer = 10f;
    [SerializeField] Image timerImage;
    public bool isAnsweringQuestion = false;
    float timerValue;

    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue < 0 && isAnsweringQuestion)
        {
            isAnsweringQuestion = false;
            timerValue = timeToShowAnswer;
        }
        else if (timerValue < 0 && !isAnsweringQuestion)
        {
            isAnsweringQuestion = true;
            timerValue = timeToCompleteQuestion;
        }
    }
}
