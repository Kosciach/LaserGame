using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuMenager : MonoBehaviour
{
    //[Header("----TextMeshPro----------------")]
    //[SerializeField] TextMeshProUGUI _

    [Header("----Reference----------------")]
    [SerializeField] TransitionerScript _transitionerScript;
    [Header("----Screens----------------")]
    [SerializeField] GameObject _mainScreen;
    [SerializeField] GameObject _optionsScreen;

    private void Awake()
    {
        _mainScreen = FindObjectOfType<Canvas>().transform.GetChild(0).gameObject;
        _optionsScreen = FindObjectOfType<Canvas>().transform.GetChild(1).gameObject;
        _transitionerScript = FindObjectOfType<TransitionerScript>();
        //if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
    }

    private void Start()
    {
        SwitchScreens(_mainScreen);
    }


    public void SwitchScreens(GameObject activeScreen)
    {
        _mainScreen.SetActive(false);
        _optionsScreen.SetActive(false);

        activeScreen.SetActive(true);
    }

    public void SwitchScenes(string newScene)
    {
        _transitionerScript.StartTransition(newScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
