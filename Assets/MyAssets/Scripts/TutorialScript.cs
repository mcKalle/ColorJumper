using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MyAssets.Scripts
{
    public class TutorialScript : MonoBehaviour
    {
        public GameObject playerObject;

        PlayerController playerController;

        bool tutorialFinished;

        [HideInInspector]
        public bool tutorialGoalColliderHit = false;

        int tutorialStep = 0;

        [Header("Tutorial UI")]
        public GameObject tutorialPanel;
        public TextMeshProUGUI tutorialText;
        public GameObject colorChangerArrow;
        public GameObject powerUpArrow;

        public GameObject[] usePowerUpArrows = new GameObject[3];

        public GameObject tutorialColorChanger;

        float fadeInDuration = 0.8f;
        float arrowTargetOpacity = .56f;

        private void Awake()
        {
            playerController = playerObject.GetComponent<PlayerController>();

            // read from settings if tutorial was already finished
            tutorialFinished = PlayerPrefs.GetInt("TutorialDone") == 1;
            if (!tutorialFinished)
            {
                playerController.moveSidewaysAllowed = false;
                tutorialText.gameObject.SetActive(true);
                tutorialPanel.SetActive(true);

                // set opacity of hint sprites to 0 (to later fade them in)
                var spriteRenderer = tutorialColorChanger.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
                spriteRenderer = colorChangerArrow.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
                spriteRenderer = powerUpArrow.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

                foreach (var arrowObject in usePowerUpArrows)
                {
                    spriteRenderer = arrowObject.GetComponent<SpriteRenderer>();
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
                }
            }
            else
            {
                tutorialText.gameObject.SetActive(false);
                tutorialPanel.SetActive(false);
                colorChangerArrow.SetActive(false);
                powerUpArrow.SetActive(false);
            }
        }

        void Update()
        {
            if (!tutorialFinished)
            {
                // steps for the tutorial
                switch (tutorialStep)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            // player managed jump
                            tutorialText.text = "Double tap for Double jump.";

                            tutorialStep++;
                        }
                        break;
                    case 1:
                        if (playerController.jumpCount == 2)
                        {
                            // player managed double jump 

                            FadeInGameObject(tutorialColorChanger, fadeInDuration, 1f);
                            FadeInGameObject(colorChangerArrow, fadeInDuration, arrowTargetOpacity);

                            tutorialText.text = "Use A and D to move.\nJump into the Color Changer to switch to a different color world.";

                            // player movement is now activateds
                            playerController.moveSidewaysAllowed = true;

                            tutorialStep++;
                        }
                        break;
                    case 2:
                        if (GameManager.Instance.IsColor1)
                        {
                            // color was changed
                            colorChangerArrow.SetActive(false);
                            FadeInGameObject(powerUpArrow, fadeInDuration, arrowTargetOpacity);

                            tutorialText.text = "Pick up the power up.";

                            tutorialStep++;
                        }
                        break;
                    case 3:
                        if (playerController.splitPowerUp.Count > 0)
                        {
                            // player picked up the power up
                            powerUpArrow.SetActive(false);

                            foreach (var arrowObject in usePowerUpArrows)
                            {
                                FadeInGameObject(arrowObject, fadeInDuration, arrowTargetOpacity);
                            }

                            tutorialText.text = "Tap the Enter key to use the power up in order to reach the platform.";

                            tutorialStep++;
                        }
                        break;
                    case 4:
                        if (tutorialGoalColliderHit)
                        {
                            foreach (var arrowObject in usePowerUpArrows)
                            {
                                arrowObject.SetActive(false);
                            }

                            tutorialText.text = "Finish the level and enter the goal.";

                            tutorialFinished = true;

                            // save finishing of tut
                            //PlayerPrefs.SetInt("TutorialDone", 1);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        void FadeInGameObject(GameObject gameObjectToFade, float duration, float targetOpacity)
        {
            StartCoroutine(FadeIn(gameObjectToFade, duration, targetOpacity));
        }

        IEnumerator FadeIn(GameObject gameObjectToFade, float duration, float targetOpacity)
        {
            SpriteRenderer spriteRenderer = gameObjectToFade.GetComponent<SpriteRenderer>();

            // Cache the current color of the material, and its initiql opacity.
            Color color = spriteRenderer.color;
            float startOpacity = color.a;

            // Track how many seconds we've been fading.
            float t = 0;

            while (t < duration)
            {
                // Step the fade forward one frame.
                t += Time.deltaTime;
                // Turn the time into an interpolation factor between 0 and 1.
                float blend = Mathf.Clamp01(t / duration);

                // Blend to the corresponding opacity between start & target.
                color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

                // Apply the resulting color to the material.
                spriteRenderer.color = color;

                // Wait one frame, and repeat.
                yield return null;
            }
        }

    }
}
