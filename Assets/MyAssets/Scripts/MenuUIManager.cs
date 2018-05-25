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

    [Header("Sounds")]
    public AudioSource MenuAudioSource;
    public AudioClip ButtonHoverSound;

    [Header("UI Animation")]
    public Transform fadeLeftAnimationOrigin;
    public Transform fadeRightAnimationOrigin;
    public Transform centerScreenTransform;
    public float smoothTime = 0.1f;
    public float fadeVelocity = 5f;

    bool playFadeOutAnimation;
    bool playFadeInAnimation;

    Vector3 destinationFadeOutPosition;
    Transform screenToFadeOut;

    Vector3 destinationFadeInPosition;
    Transform screenToFadeIn;

    Vector3 velocity = Vector3.zero;

    GameObject activeScreen;

    // Use this for initialization
    void Start()
    {
        StartScreen.SetActive(true);
        PlayScreen.SetActive(true);
        ScoresScreen.SetActive(true);
        OptionsScreens.SetActive(true);

        PlayScreen.transform.SetPositionAndRotation(fadeRightAnimationOrigin.position, fadeRightAnimationOrigin.rotation);

        ScoresScreen.transform.SetPositionAndRotation(fadeRightAnimationOrigin.position, fadeRightAnimationOrigin.rotation);

        OptionsScreens.transform.SetPositionAndRotation(fadeRightAnimationOrigin.position, fadeRightAnimationOrigin.rotation);
    }


    public void ShowMenuScreen(string title)
    {
        switch (title)
        {
            case "Start":
                FadeOutMenuScreen(activeScreen, fadeRightAnimationOrigin);
                FadeInMenuScreen(StartScreen, fadeLeftAnimationOrigin);

                activeScreen = StartScreen;

                break;
            case "Play":
                FadeOutMenuScreen(StartScreen, fadeLeftAnimationOrigin);
                FadeInMenuScreen(PlayScreen, fadeRightAnimationOrigin);

                activeScreen = PlayScreen;

                // TODO 
                // read active levels

                break;
            case "Scores":
                FadeOutMenuScreen(StartScreen, fadeLeftAnimationOrigin);
                FadeInMenuScreen(ScoresScreen, fadeRightAnimationOrigin);

                activeScreen = ScoresScreen;

                // TODO 
                // read score

                break;
            case "Options":
                FadeOutMenuScreen(StartScreen, fadeLeftAnimationOrigin);
                FadeInMenuScreen(OptionsScreens, fadeRightAnimationOrigin);

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

    private void FadeOutMenuScreen(GameObject screen, Transform sideToFadeOut)
    {
        screenToFadeOut = screen.transform;
        destinationFadeOutPosition = sideToFadeOut.position;
        playFadeOutAnimation = true;
    }

    private void FadeInMenuScreen(GameObject screen, Transform sideToFadeOut)
    {
        screenToFadeIn = screen.transform;
        destinationFadeInPosition = centerScreenTransform.position;
        playFadeInAnimation = true;
    }

    private void FixedUpdate()
    {
        if (playFadeOutAnimation)
        {
            screenToFadeOut.position = Vector3.SmoothDamp(screenToFadeOut.position, destinationFadeOutPosition, ref velocity, smoothTime, fadeVelocity);

            if (screenToFadeOut.position == destinationFadeOutPosition)
            {
                playFadeOutAnimation = false;
            }
        }

        if (playFadeInAnimation)
        {
            screenToFadeIn.position = Vector3.SmoothDamp(screenToFadeIn.position, destinationFadeInPosition, ref velocity, smoothTime, fadeVelocity);

            if (screenToFadeIn.position == destinationFadeInPosition)
            {
                playFadeInAnimation = false;
            }
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
