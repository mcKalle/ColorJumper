﻿using Assets.MyAssets.Scripts.PowerUps;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Camera MainCamera;

    public static GameManager Instance;

    [Header("Background Colors")]
    public Color Color1;
    public Color Color2;

    [Header("Obstacle Containers")]
    public GameObject Color1Obstacles;
    public GameObject Color2Obstacles;

    [Header("Player Materials")]
    public Material Color1Material;
    public Material Color2Material;

    [Header("Label Colors")]
    public Color LabelColor1;
    public Color LabelColor2;

    [Header("Start Point")]
    public Transform LevelEntryPoint;


    GameObject player;
    Renderer playerRenderer;

    [HideInInspector]
    public bool IsColor1 = true;

    int changeCount;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        changeCount = 0;

        player = GameObject.FindGameObjectWithTag(ColorJumperConstants.PLAYER);
        playerRenderer = player.GetComponent<Renderer>();

        Respawn();
    }

    #region SpawnDeathFinish

    public void ApplyPlayerDeath()
    {
        MainCamera.backgroundColor = Color.black;

        playerRenderer.enabled = false;

        Respawn();
    }

    private void Respawn()
    {
        player.transform.position = LevelEntryPoint.position;

        ChangeColor(2);
        changeCount = 0;
        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateChangeCount(changeCount);
            UIManager.Instance.UpdateSplitPowerUpCount(0);
        }

        playerRenderer.enabled = true;
        
        player.GetComponent<BoxCollider2D>().transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y + 5), player.transform.rotation);
    }

    public void Finish()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string lastChar = currentScene[currentScene.Length - 1].ToString();

        int level = Convert.ToInt32(lastChar);

        SceneManager.LoadScene("Level " + (level + 1));
    }

    #endregion

    #region ColorChanging

    public void ChangeColor(int colorMode)
    {
        IsColor1 = !IsColor1;

        switch (colorMode)
        {
            case 1:
                MainCamera.backgroundColor = Color2;
                playerRenderer.material = Color2Material;
                Color1Obstacles.SetActive(false);
                Color2Obstacles.SetActive(true);
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.ApplyColorChange(LabelColor2, colorMode);
                }
                break;
            case 2:
                MainCamera.backgroundColor = Color1;
                playerRenderer.material = Color1Material;
                Color1Obstacles.SetActive(true);
                Color2Obstacles.SetActive(false);
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.ApplyColorChange(LabelColor1, colorMode);
                }
                break;
            default:
                break;
        }

        changeCount++;

        // UI handling
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateChangeCount(changeCount);
        }

    }

    #endregion

    #region PowerUps

    public void TakePowerUp(GameObject gameObject, IPowerUp powerUp)
    {
        if (powerUp is SplitPowerUp)
        {
            powerUp.Count++;

            // do ui stuff
            UIManager.Instance.UpdateSplitPowerUpCount(powerUp.Count);
        }

        // let it disappear
        Destroy(gameObject, 0f);
        // TODO: maybe with particles
    }

    #endregion
}
