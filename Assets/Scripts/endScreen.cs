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
        scoreText.text = "�����մϴ�!\n" + scorekeeper.CaculateScore() + "%�� ������ �����̽��ϴ�.";
    }
}
