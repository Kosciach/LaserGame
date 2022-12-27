using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [Header("----References-------------")]
    [SerializeField] TextMeshProUGUI _scoreText;

    [Header("----Values-------------")]
    [SerializeField] int _score;

    private void Start()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }
    public void UpdateScore(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }
}
