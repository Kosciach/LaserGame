using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuMenager : MonoBehaviour
{
    [Header("----TextMeshPro----------------")]
    [SerializeField] TextMeshProUGUI _highScoreText;

    [Header("----Reference----------------")]
    [SerializeField] Image _fadeImage;
    [SerializeField] Canvas _mainCanvas;

    [Header("----Screens----------------")]
    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _optionsScreen;
    [SerializeField] GameObject _creditsScreen;

    [Header("----TransitionValues----------------")]
    [SerializeField] float _fadeSpeed;

    private string _newScene;

    private void Awake()
    {
        _mainScreen = _mainCanvas.transform.GetChild(0).gameObject;
        _optionsScreen = _mainCanvas.transform.GetChild(1).gameObject;
        if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
    }

    private void Start()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        LeanTween.alpha(_fadeImage.rectTransform, 0f, _fadeSpeed);
        SwitchScreens(_mainScreen);
    }


    public void SwitchScreens(GameObject activeScreen)
    {
        _mainScreen.SetActive(false);
        _optionsScreen.SetActive(false);
        _creditsScreen.SetActive(false);

        activeScreen.SetActive(true);
    }

    public void SwitchScenes(string newScene)
    {
        Debug.Log("elo");
        _newScene = newScene;
        LeanTween.alpha(_fadeImage.rectTransform, 1f, _fadeSpeed).setOnComplete(FadeComplete);
    }
    private void FadeComplete()
    {
        SceneManager.LoadScene(_newScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
