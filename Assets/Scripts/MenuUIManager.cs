using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{

    [Header("Menu Screens")]
    public GameObject StartScreen;
    public GameObject PlayScreen;
    public GameObject ScoresScreen;
    public GameObject OptionsScreens;

    private GameObject activeScreen;

    // Use this for initialization
    void Start()
    {
        activeScreen = StartScreen;

        StartScreen.SetActive(true);
        PlayScreen.SetActive(false);
        ScoresScreen.SetActive(false);
        OptionsScreens.SetActive(false);
    }


    public void ShowMenuScreen(string title)
    {
        switch (title)
        {
            case "Start":
                activeScreen.SetActive(false);
                StartScreen.SetActive(true);
                activeScreen = StartScreen;
                break;
            case "Play":
                activeScreen.SetActive(false);
                PlayScreen.SetActive(true);
                activeScreen = PlayScreen;

                // TODO 
                // read active levels

                break;
            case "Scores":
                activeScreen.SetActive(false);
                ScoresScreen.SetActive(true);
                activeScreen = ScoresScreen;

                // TODO 
                // read score

                break;
            case "Options":
                activeScreen.SetActive(false);
                OptionsScreens.SetActive(true);
                activeScreen = OptionsScreens;

                // TODO 
                // read settings

                break;
            default:
                // Exit
                Application.Quit();
                break;
        }
    }


    #region PlayScreen

    public void OpenLevel(int number)
    {
        SceneManager.LoadScene("Level " + number);
    }

    #endregion


    #region OptionsScreen

    #endregion


    #region ScoresScreen

    #endregion
}
