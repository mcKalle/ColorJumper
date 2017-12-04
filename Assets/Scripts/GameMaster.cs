using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public Camera MainCamera;

    public static GameMaster Instance;

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

    static bool isColor1 = true;
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
        }

        playerRenderer.enabled = true;
        // MainCamera.transform.SetPositionAndRotation(new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 200), MainCamera.transform.rotation);

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
}
