using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameplayMenager : MonoBehaviour
{
    [Header("----Reference----------------")]
    [SerializeField] CanvasGroup _fadeCanvas;
    [SerializeField] Canvas _mainCanvas;

    [Header("----TransitionValues----------------")]
    [SerializeField] float _fadeSpeed;

    private string _newScene;

    private void Awake()
    {
        //if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
    }

    private void Start()
    {
        LeanTween.alphaCanvas(_fadeCanvas, 0f, _fadeSpeed);
    }

    public void SwitchScenes(string newScene)
    {
        Debug.Log("elo");
        _newScene = newScene;
        LeanTween.alphaCanvas(_fadeCanvas, 1f, _fadeSpeed).setOnComplete(FadeComplete);
    }
    private void FadeComplete()
    {
        SceneManager.LoadScene(_newScene);
    }
}
