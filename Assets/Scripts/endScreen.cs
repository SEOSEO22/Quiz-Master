using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scorekeeper;

    private void Start()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void showFinalScore()
    {
        scoreText.text = "축하합니다!\n" + scorekeeper.CaculateScore() + "%의 문제를 맞히셨습니다.";
    }
}
