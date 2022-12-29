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
        if(PlayerPrefs.HasKey("HighScore")) _scoreText.text = _score.ToString()+"|"+PlayerPrefs.GetInt("HighScore").ToString();
        else _scoreText.text = _score.ToString() + "|" + _score.ToString();
    }
    public void UpdateScore(int points)
    {
        _score += points;
        if (_score > PlayerPrefs.GetInt("HighScore")) PlayerPrefs.SetInt("HighScore", _score);
        _scoreText.text = _score.ToString() + "|" + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
